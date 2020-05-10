using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SETA.Common;
using SETA.Common.Constants;
using SETA.Core.Base;
using SETA.Core.Data;
using SETA.DataAccess.Interface;
using SETA.Entity;

namespace SETA.DataAccess
{
    public class CategoryDal : BaseDal<ADOProvider>, IDataAccess<Category>
    {
        public Category Get(long id)
        {
            try
            {
                return unitOfWork.Procedure<Category>("pro_Get_CategoryInfo",
                                                                 new
                                                                 {
                                                                     CategoryID = id
                                                                 }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public IList<Category> Get(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CategoryName", listParam.Keyword);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var values = unitOfWork.Procedure<Category>("pro_Get_Categorys", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception)
            {
                totalRecord = 0;
                return new List<Category>();
            }
        }

        public int GetCountAllCategory()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StatusIDActive", GlobalStatus.Active);
                var values = unitOfWork.Procedure<int>("pro_Get_Categories_Count_All", param).FirstOrDefault();
                return values;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList<Category> GetAllCateWithCountProduct()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StatusIDActive", GlobalStatus.Active);
                var values = unitOfWork.Procedure<Category>("pro_Get_Categories_With_CountProduct", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return new List<Category>();
            }
        }

        public long Save(Category obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userID);
                param.Add("@XML", XMLHelper.SerializeXML<Category>(obj));
                param.Add("@CategoryID", obj.CategoryID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                var flag = unitOfWork.ProcedureExecute("pro_Update_CategoryInfo", param);
                return param.Get<long>("@CategoryID");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        public long SaveVietnamese(Category obj, long userId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userId);
                param.Add("@XML", XMLHelper.SerializeXML<Category>(obj));
                param.Add("@CategoryID", obj.CategoryID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                param.Add("@CategoryName", obj.CategoryName);
                param.Add("@Description", obj.Description);
                var flag = unitOfWork.ProcedureExecute("pro_Update_CategoryInfo_Vietnamese", param);
                return param.Get<long>("@CategoryID");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        public bool Delete(long objId, long userID)
        {
            try
            {
                return unitOfWork.ProcedureExecute("pro_Delete_Category", new { CategoryID = objId, UserID = userID });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return false;
        }
    }
}
