using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace SETA.Core.Repository
{
    /// <summary>
    /// Repository Entity DbContext
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// Link ref : http://blog.longle.net/2013/05/11/genericizing-the-unit-of-work-pattern-repository-pattern-with-entity-framework-in-mvc/
    /// Link ref : http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual TEntity FindById(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Inserts the graph.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            if (entity != null)
                DbSet.Add(entity);
        }

        //public virtual void InsertGraph(TEntity entity)
        //{
        //    DbSet.Add(entity);
        //}

        //public virtual void Insert(TEntity entity)
        //{
        //    DbSet.Attach(entity);
        //}

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            //if (entity == null)
            //{
            //    throw new ArgumentException("Cannot add a null entity.");
            //}

            //var manager = ((IObjectContextAdapter)dbContext).ObjectContext.ObjectStateManager;

            //var isPresent = ExistsAndDetach(entity);

            //var stateCurrent = Context.Entry(entity).State;
            //if (stateCurrent == EntityState.Detached && isPresent == false)

            //if (isPresent == false) // if not exists
            //{
            //    DbSet.Attach(entity);
            //}
            //else
            //{
            //    Context.Entry(entity).CurrentValues.SetValues(entity);
            //}

            var entryCurrent = Context.Entry(entity);
            var key = this.GetPrimaryKey(entryCurrent);

            if (entryCurrent.State == EntityState.Detached)
            {
                var currentEntry = DbSet.Find(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.Context.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    this.DbSet.Attach(entity);
                    entryCurrent.State = EntityState.Modified;
                }
            }

            //if (entity != null)
            //{
            //    DbSet.Attach(entity);
            //    Context.Entry(entity).State = EntityState.Modified;
            //}
        }
        public virtual void Update(TEntity entity, string keyName)
        {
            //if (entity == null)
            //{
            //    throw new ArgumentException("Cannot add a null entity.");
            //}
            //check existed in dbset
            if (entity != null)
            {
                if (Context.Entry(entity).State != EntityState.Detached) return;
                var set = Context.Set<TEntity>();
                var pkey = DbSet.Create().GetType().GetProperty(keyName).GetValue(entity);
                TEntity attachEntity = set.Find(pkey);
                if (attachEntity != null)
                {
                    var attactedEntry = Context.Entry(attachEntity);
                    attactedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    Context.Entry(entity).State = EntityState.Modified;
                }
            }
        }

        #region Change state of entity

        /// <summary>
        /// Change state Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AddState(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// Change state Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateState(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Change state Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteState(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }
        #endregion

        public virtual void ReloadEntity(TEntity entity)
        {
            Context.Entry(entity).Reload();
        }

        public virtual void EmptyDbSetLocal()
        {
            DbSet.Local.Clear();
        }

        public virtual void RemoveEntity(TEntity entity)
        {
            //DbSet.Local.Remove(entity);
        }

        /// <summary>
        /// Existses the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Exists(TEntity entity)
        {
            return DbSet.Local.Any(e => e == entity);
        }


        /// <summary>
        /// Existses the specified keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">The keys.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Exists<T>(params object[] keys)
        {
            return (DbSet.Find(keys) != null);
        }

        /// <summary>
        /// Lasts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>`0.</returns>
        public TEntity Last(TEntity entity)
        {
            return DbSet.Local.Last();
        }

        /// <summary>
        /// Existses the and detach.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool ExistsAndDetach(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)this.Context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            // TryGetObjectByKey attaches a found entity
            // Detach it here to prevent side-effects

            if (exists)
            {
                Context.Entry(entity).State = EntityState.Detached;
                //objContext.Detach(foundEntity);
            }

            return (exists);
        }

        public bool ExistsInDatabase<T>(T obj) where T : class
        {
            //bool exists = false;
            //var ctx = ((IObjectContextAdapter)this).ObjectContext;
            //var entry = this.Entry(obj);
            //if (entry.State != EntityState.Detached)
            //{
            //    var set = Context.CreateObjectSet<T>().EntitySet;
            //    var keyprop = set.ElementType.KeyMembers.First();
            //    //if the key is integer we can check based on if id > 0 or not
            //    if (keyprop.TypeUsage.EdmType.Name == "Int32")
            //    {
            //        int keyval = entry.CurrentValues.GetValue<int>(keyprop.Name);
            //        if (keyval > 0)
            //        {
            //            exists = true;
            //        }
            //    }
            //    else
            //    {
            //        var databasevalues = entry.GetDatabaseValues();
            //        if (databasevalues != null)
            //        {
            //            exists = true;
            //        }
            //    }
            //}

            //using
            /*
             var db = new NorthWindEntities();
            var cat = db.Categories.AsNoTracking().First();
            db.Entry<Category>(cat).State = System.Data.EntityState.Added;
            var isnew = db.ExistsInDatabase<Category>(cat);
            Console.WriteLine(isnew);
             */

            //return exists;
            return false;
        }


        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public virtual void Delete(object id)
        {
            var entity = DbSet.Find(id);
            var objectState = entity as IObjectState;
            if (objectState != null)
                objectState.State = ObjectState.Deleted;
            Delete(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity != null)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }


        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <returns></returns>
        public virtual IDbSet<TEntity> Query()
        {
            return DbSet;
        }

        /// <summary>
        /// Queries the no tracking.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> QueryNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// Queries the repos.
        /// </summary>
        /// <returns></returns>
        public virtual RepositoryQuery<TEntity> QueryRepos()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>>
                includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);


            //For performance http://blogs.msdn.com/b/wriju/archive/2010/07/09/ef4-use-mergeoption-notracking-for-better-query-performance.aspx
            query.AsNoTracking();

            return query;
        }

        /// <summary>
        /// Retrieve the underlying ObjectContext
        /// </summary>
        public ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)this).ObjectContext;
            }
        }

        public ObjectContext GetObjectContext()
        {
            return ObjectContext;
        }

        public void Rollback() { Context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload()); }

        /// <summary>
        /// Gets the with raw SQL.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters).ToList(); //Dbcontext have sql Query, IDbContext not have
            //return null;
        }
        public virtual bool TruncateTable(string command)
        {
            var total = Context.Database.SqlQuery<int>(command).SingleOrDefault();
            return total <= 0;
        }

        public void CheckStateObjectChange()
        {
            var listObject = Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
        }

        private int GetPrimaryKey(DbEntityEntry entry)
        {
            var myObject = entry.Entity;
            var property =
                myObject.GetType()
                     .GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            if (property != null) return (int)property.GetValue(myObject, null);
            return 0;
        }

        #region handler local data
        //http://stackoverflow.com/questions/11536963/delete-item-in-database-when-removed-from-collection-ef-4-3
        private void LocalPlansChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
                return;

            var a = e.NewItems;
            //foreach (var workPlan in e.NewItems.Cast<WorkPlan>())
            //{
            //    var collection = workPlan.Breaks as EntityCollection<Break>;
            //    if (collection == null)
            //        continue;
            //    collection.AssociationChanged += BreaksAssociationChanged;
            //}
        }

        private void BreaksAssociationChanged(object sender, CollectionChangeEventArgs e)
        {
            //if (e.Action == CollectionChangeAction.Remove)
            //{
            //    var @break = (Break)e.Element;
            //    Breaks.Remove(@break);
            //}
        }
        #endregion
    }
}
