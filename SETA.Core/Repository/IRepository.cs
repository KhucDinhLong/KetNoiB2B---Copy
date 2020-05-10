using System.Data.Entity;
using System.Linq;
using System.Data.Objects;

namespace SETA.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Update(TEntity entity, string primaryKeyName);
        void Delete(object id);
        void Delete(TEntity entity);
        RepositoryQuery<TEntity> QueryRepos();
        IDbSet<TEntity> Query();
        IQueryable<TEntity> QueryNoTracking();
        void EmptyDbSetLocal();
        void RemoveEntity(TEntity entity);
        ObjectContext GetObjectContext();
        bool TruncateTable(string command);
        void CheckStateObjectChange();
        void AddState(TEntity item);
        void UpdateState(TEntity item);
        void DeleteState(TEntity item);
    }
}
