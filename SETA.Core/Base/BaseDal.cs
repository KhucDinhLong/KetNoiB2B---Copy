using SETA.Core.Caching;
using SETA.Core.Singleton;
using SETA.Core.Helper.Cache;

namespace SETA.Core.Base
{
    public class BaseDal<T>
    {
        public T unitOfWork;
        protected ICacheProvider cache;
        protected CacheHelper cacheHelper;
        //protected string _schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDal{T}" /> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public BaseDal()
        {
            //_schema = schema;
            //cache = new MemcachedProvider(schema);
            //cacheHelper = new CacheHelper(schema);
            unitOfWork = (T) SingletonIpl.GetInstance<T>();
            //unitOfWork.CacheHelper = cacheHelper;
        }
    }
}
