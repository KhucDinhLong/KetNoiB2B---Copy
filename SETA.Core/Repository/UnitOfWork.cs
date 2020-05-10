using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Transactions;
using System.Web;
using Newtonsoft.Json;
using SETA.Core.Configuration;
using SETA.Core.EF;
using SETA.Core.Helper.Session;
using log4net;

namespace SETA.Core.Repository
{
    /// <summary>
    /// Unit of Work (UoW) for Entity Framework SETA CRM
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseCRMContext Context;
        public string Schema;
        private static readonly ILog MemoryUse = LogManager.GetLogger("MemoryUseLogger");
        //public readonly IDbContext Context;

        private bool _disposed;
        public Hashtable Repositories;

        public UnitOfWork(string schema)
        {
            SetContextUoW(schema);
        }

        public UnitOfWork SetContextUoW(string schema)
        {
            Context = DatabaseCRMContext.GetContext(schema);
            Schema = schema;
            return this;
        }

        /// <summary>
        /// Changes the config context.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public UnitOfWork ChangeConfigContext(DbContextConfig config = null)
        {
            if (config != null) Context.ChangeConfig(config);
            return this;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            var status = 0;

            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            //using (var scope = Context.Database.Connection.BeginTransaction())
            {
                Context.ChangeTracker.DetectChanges(); //Manually detect changes

                var changedEntries = Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
                try
                {
                    if (Context.Database.Connection.State != ConnectionState.Open)
                        Context.Database.Connection.Open();

                    #region Common Last Modify
                    UserSession sessUser = null;
                    if (HttpContext.Current != null && HttpContext.Current.Session != null
                        && HttpContext.Current.Session.Count > 0 && HttpContext.Current.Session[Config.GetConfigByKey("SessionKeyUser")] != null)
                    {
                        var keySession = Config.GetConfigByKey("SessionKeyUser");
                        //var json_serializer = new JavaScriptSerializer();
                        var json = JsonConvert.SerializeObject(HttpContext.Current.Session[keySession], Formatting.Indented);
                        if (!string.IsNullOrEmpty(json))
                        {
                            sessUser = JsonConvert.DeserializeObject<UserSession>(json);
                        }
                        //sessUser = HttpContext.Current.Session[keySession] as UserSession;
                        //http://msdn.microsoft.com/en-us/data/jj592677.aspx
                    }

                    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
                    {
                        var entiyPropertyNames = entry.CurrentValues;
                        //var entiyPropertyNames = Context.Entry(entry).CurrentValues;
                        foreach (var propertyName in entiyPropertyNames.PropertyNames)
                        {
                            if (propertyName == "LastModifiedDate")
                            {
                                entry.Property("LastModifiedDate").CurrentValue = DateTime.Now;
                            }

                            if (propertyName == "LastModifiedBy" && sessUser != null && sessUser.Id > 0)
                            {
                                entry.Property("LastModifiedBy").CurrentValue = sessUser.Id;
                            }
                        }
                    }
                    #endregion

                    //http://entityframework.codeplex.com/workitem/1605
                    MemoryUse.Info(String.Format("EF version: {0}", typeof(DbContext).Assembly.GetName().Version));
                    MemoryUse.Info(String.Format("Before Save...{0:N0}", GC.GetTotalMemory(true)));

                    //Context.Configuration.AutoDetectChangesEnabled = true;
                    //http://stackoverflow.com/questions/10103310/dbcontext-is-very-slow-when-adding-and-deleting
                    status = Context.SaveChanges();
                    Context.Configuration.AutoDetectChangesEnabled = false;

                    MemoryUse.Info(String.Format("After Save...{0:N0}", GC.GetTotalMemory(true)));
                }
                catch (DbEntityValidationException ex)
                {
                    #region DbEntityValidationException
                    //var changedEntries = Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
                    //changedEntries.Clear();
                    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
                    {
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                    }

                    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
                    {
                        entry.State = EntityState.Detached;
                    }

                    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    EFValidation.LogValidationErrors(ex);
                    #endregion
                }
                catch (Exception ex)
                {
                    #region Exception
                    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
                    {
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                    }

                    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
                    {
                        entry.State = EntityState.Detached;
                    }

                    foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    Helper.Logging.Logging.PutError("DbEntityException Save Data: ", ex);
                    #endregion
                }
                finally
                {
                    if (Context.Database.Connection.State != ConnectionState.Closed)
                        Context.Database.Connection.Close();
                }

                //Replace the flag condition with the one you need 
                //or remove it and leave only commit part
                //if (status != -1)
                //{
                //    //transaction completed successfully, both calls succeeded
                //    scope.Commit();
                //}
                //else
                //{
                //    //something is wrong, both calls are rolled back
                //    scope.Rollback();
                //}
                scope.Complete();
            }
            return status;
            //return Context.SaveChanges();
        }

        public void DebugChangeTracker()
        {
            //var changedEntries = Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            var changedEntries = Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            //var path = @"C:\mypath\";
            //path = path + Util.GetMsSinceEpoch().ToString() + "changeTracker.log";

            //using (StreamWriter sw = new StreamWriter(path))
            //{
            //    var changeTracker = db.ChangeTracker;
            //    var entries = changeTracker.Entries();
            //    foreach (var x in entries)
            //    {

            //        var name = x.Entity.ToString();
            //        var state = x.State;

            //        sw.WriteLine("");
            //        sw.WriteLine("***Entity Name: " + name + "is in a state of " + state);
            //        var currentValues = x.CurrentValues;
            //        sw.WriteLine("***CurrentValues***");
            //        PrintPropertyValues(currentValues, sw);
            //        if (state != EntityState.Added)
            //        {
            //            sw.WriteLine("***Original Values***");
            //            PrintPropertyValues(x.OriginalValues, sw);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Repositories this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (Repositories == null)
                Repositories = new Hashtable();

            var type = typeof(T).Name;

            type = Schema + "_" + type;

            if (!Repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), Context);

                Repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)Repositories[type];
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IRepository<T> GetRepository<T>(string key) where T : class
        {
            if (Repositories.ContainsKey(key))
            {
                return (IRepository<T>)Repositories[key];
            }
            return null;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing && Context != null)
                    Context.Dispose();

            _disposed = true;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() { Dispose(false); }
    }
}
