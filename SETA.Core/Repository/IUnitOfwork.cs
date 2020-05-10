using System;

namespace SETA.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Dispose();
        int Save();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : class;
    }
}
