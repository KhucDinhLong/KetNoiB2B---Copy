using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETA.Entity;

namespace SETA.BusinessLogic.Interface
{
    interface IBusinessLogic
    {
    }
    /// <summary>
    /// Business Logic Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBusinessLogic<T>
    {
        T Get(long id);
        IList<T> Get(BaseListParam param, out int? totalRecord);
        long Save(T obj, long userID);
        bool Delete(long objId, long userID);
    }
    public interface IBusinessLogicSmall<T>
    {
        T Get(int id);
        IList<T> Get(BaseListParam param, out int? totalRecord);
        int Save(T obj, long userID);
        bool Delete(int objId, long userID);
    }
}
