using System;
using System.Collections.Generic;
using SETA.Entity;

namespace SETA.DataAccess.Interface
{
    /// <summary>
    /// Data Access Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMemberDataAccess<T>
    {
        T Get(long id);
        IList<T> Get(MemberListParam param, out int? totalRecord);
        long Save(T obj, long userID);
        bool Delete(long objId, long userID);
    }
}
