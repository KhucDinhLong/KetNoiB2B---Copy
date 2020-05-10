using System;
using System.Collections.Generic;
using SETA.Entity;

namespace SETA.BusinessLogic.Interface
{
    interface IMember
    {
    }
    /// <summary>
    /// Business Logic Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMember<T>
    {
        T Get(Guid id);
        IList<T> Get(BaseListParam param, out int? totalRecord);
        int Save(T obj, Guid userID);
        bool Delete(int objId, Guid userID);
    }
}
