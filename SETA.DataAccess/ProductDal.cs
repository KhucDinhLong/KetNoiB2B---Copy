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
    public class ProductDal : BaseDal<ADOProvider>, IDataAccess<Product>
    {
        public Product Get(long id)
        {
            try
            {
                return unitOfWork.Procedure<Product>("pro_Get_ProductInfo",
                                                                 new
                                                                 {
                                                                     ProductID = id
                                                                 }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Product();
            }
        }

        public IList<Product> Get(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CategoryID", listParam.Keyword);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var values = unitOfWork.Procedure<Product>("pro_Get_Products", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<Product>();
            }
        }

        public IList<Product> GetProductHomeCarousel()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StatusIDActive", GlobalStatus.Active);                
                var values = unitOfWork.Procedure<Product>("pro_Get_Products_Home_Carousel", param).ToList();                
                return values;
            }
            catch (Exception)
            {                
                return new List<Product>();
            }
        }

        public IList<Product> GetProductHomeNewest()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StatusIDActive", GlobalStatus.Active);
                var values = unitOfWork.Procedure<Product>("pro_Get_Products_Home_Newest", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public int GetCountAllProduct()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StatusIDActive", GlobalStatus.Active);
                var values = unitOfWork.Procedure<int>("pro_Get_Products_Count_All", param).FirstOrDefault();
                return values;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public long Save(Product obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userID);
                param.Add("@XML", XMLHelper.SerializeXML<Product>(obj));
                param.Add("@ProductID", obj.ProductID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                var flag = unitOfWork.ProcedureExecute("pro_Update_ProductInfo", param);
                return param.Get<long>("@ProductID");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        public long SaveUnicode(Product obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userID);
                param.Add("@XML", XMLHelper.SerializeXML<Product>(obj));
                param.Add("@ProductID", obj.ProductID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                param.Add("@ProductName", obj.ProductName);
                param.Add("@Description", obj.Description);
                param.Add("@Unit", obj.Unit);
                param.Add("@ColorDescription", obj.ColorDescription);
                var flag = unitOfWork.ProcedureExecute("pro_Update_ProductInfo_Unicode", param);
                return param.Get<long>("@ProductID");
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
                return unitOfWork.ProcedureExecute("pro_Delete_Product", new { ProductID = objId, UserID = userID, StatusIDDeleted = GlobalStatus.Deleted });
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
