using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETA.Entity;

namespace SETA.BusinessLogic.Interface
{
    interface IMemberBusinessLogic
    {
    }
    /// <summary>
    /// Business Logic Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMemberBusinessLogic<T>
    {
        T Get(long id);
        IList<T> Get(MemberListParam param, out int? totalRecord);
        long Save(T obj, long userID);
        bool Delete(long objId, long userID);
    }
}
