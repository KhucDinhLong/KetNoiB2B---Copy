using System;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using SETA.Core.Configuration;
using SETA.Core.Data;
using System.Data.Entity.Migrations;
using System.Collections.Generic;

namespace SETA.Core.Repository
{
    /// <summary>
    /// Dabatabase Context CRM
    /// </summary>
    public class DatabaseCRMContext : DbContext, IDbContext, IDisposable
    {
        private readonly string _schema;

        public string schema
        {
            get
            {
                return this._schema;
            }
        }

        private static IDictionary<string, DbCompiledModel> complitedModels = new Dictionary<string, DbCompiledModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseCRMContext"/> class.
        /// </summary>
        /// <param name="lazyLoadingEnabled"></param>
        public IDbContext ChangeConfig(DbContextConfig config)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            return this;
        }

        public void DefaultConfigContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DatabaseCRMContext"/> class from being created.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="model">The model.</param>
        /// <param name="schema">The schema.</param>
        private DatabaseCRMContext(DbConnection connection, DbCompiledModel model, string schema)
            : base(connection, model, contextOwnsConnection: true)
        {
            Database.SetInitializer<DatabaseCRMContext>(null);
            _schema = schema;
            this.DefaultConfigContext();
        }

        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            //DbContextExtension.ApplyStateChanges(this);
            return base.SaveChanges();
        }

        /// <summary>
        /// Gets the compiled model.
        /// </summary>
        /// <param name="tenantSchema">The tenant schema.</param>
        /// <param name="conn">The conn.</param>
        /// <returns></returns>
        //private static DbCompiledModel GetCompiledModel(string tenantSchema, EntityConnection conn)
        //private static DbCompiledModel GetCompiledModel(string tenantSchema, SqlConnection conn)
        private static DbCompiledModel GetCompiledModel(string providerName, string providerManifest, string schema)
        {
            DbCompiledModel compiledModel = DatabaseCRMContext.complitedModels.Where(p => p.Key == schema).Select(p => p.Value).FirstOrDefault();
            if (compiledModel != null)
                return compiledModel;

            DbModelBuilder modelBuilder = new DbModelBuilder();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory + "\\bin", Config.GetConfigByKey("EntityConfiguration")));

            ContextConfiguration contextConfiguration = new ContextConfiguration();
            CompositionContainer container = new CompositionContainer(catalog);

            if (schema == "dbo")
            {
                container.ComposeExportedValue<string>("schema", DataHelper.TenantInternal());
                container.ComposeParts(contextConfiguration);
            }
            else
            {
                container.ComposeExportedValue<string>("schema", schema);
                container.ComposeParts(contextConfiguration);
            }

            foreach (var configurationdbo in contextConfiguration.ConfigurationsDbo)
                configurationdbo.AddConfiguration(modelBuilder.Configurations);
            foreach (var configuration in contextConfiguration.Configurations)
                configuration.AddConfiguration(modelBuilder.Configurations);

            DbProviderInfo providerInfo = new DbProviderInfo(providerName, providerManifest);
            DbModel dbModel = modelBuilder.Build(providerInfo);
            compiledModel = dbModel.Compile();
            DatabaseCRMContext.complitedModels.Add(schema, compiledModel);
            return compiledModel;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns></returns>
        public static DatabaseCRMContext GetContext(string schema)
        {
            string connectionString = DataHelper.GetConnectionString();
            string providerName = DataHelper.GetProviderName();
            string providerManifest = DataHelper.GetProviderManifest();

            var conn = DatabaseCRMContext.GetConnection(providerName, connectionString);
            DbCompiledModel compiledModel = GetCompiledModel(providerName, providerManifest, schema);
            var context = new DatabaseCRMContext(conn, compiledModel, schema);
            return context;
        }

        private static DbConnection GetConnection(string providerName, string connectionString)
        {
            DbConnection conn = DbProviderFactories.GetFactory(providerName).CreateConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }

        /// <summary>
        /// Calls the protected Dispose method.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// DbContextConfig Class for 
    /// </summary>
    public class DbContextConfig
    {
        public DbContextConfig()
        {
        }

        public DbContextConfig(bool lazyLoading = true, bool validationOnSave = true, bool autoDetectChange = true)
        {
            this.LazyLoadingEnabled = lazyLoading;
            this.ValidateOnSaveEnabled = validationOnSave;
            this.AutoDetectChangesEnabled = autoDetectChange;
        }

        private bool iLazyLoadingEnabled = true;
        public bool LazyLoadingEnabled
        {
            get { return iLazyLoadingEnabled; }
            set { iLazyLoadingEnabled = value; }
        }

        private bool iProxyCreationEnabled = false;
        public bool ProxyCreationEnabled
        {
            get { return iProxyCreationEnabled; }
            set { iProxyCreationEnabled = value; }
        }

        private bool iValidateOnSaveEnabled = true;
        public bool ValidateOnSaveEnabled
        {
            get { return iValidateOnSaveEnabled; }
            set { iValidateOnSaveEnabled = value; }
        }

        private bool iAutoDetectChangesEnabled = true;
        public bool AutoDetectChangesEnabled
        {
            get { return iAutoDetectChangesEnabled; }
            set { iAutoDetectChangesEnabled = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class Configuration : DbMigrationsConfiguration<DatabaseCRMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
