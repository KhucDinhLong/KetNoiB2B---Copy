using System;
using System.Collections.Generic;
using SETA.Entity;

namespace SETA.DataAccess.Interface
{
    /// <summary>
    /// Data Access Interface: Data is big
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataAccess<T>
    {
        T Get(long id);
        IList<T> Get(BaseListParam param, out int? totalRecord);
        long Save(T obj, long userID);
        bool Delete(long objId, long userID);
    }

    /// <summary>
    /// Data Access Interface: Data is small
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataAccessSmall<T>
    {
        T Get(int id);
        IList<T> Get(BaseListParam param, out int? totalRecord);
        int Save(T obj, long userID);
        bool Delete(int objId, long userID);
    }
}
