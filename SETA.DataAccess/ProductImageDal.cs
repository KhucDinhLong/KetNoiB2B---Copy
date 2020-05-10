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
    public class ProductImageDal : BaseDal<ADOProvider>, IDataAccess<ProductImage>
    {
        public ProductImage Get(long id)
        {
            try
            {
                return unitOfWork.Procedure<ProductImage>("pro_Get_Product_ImageInfo",
                                                                 new
                                                                 {
                                                                     ProductImageID = id
                                                                 }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new ProductImage();
            }
        }

        public IList<ProductImage> Get(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductID", listParam.Keyword);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var values = unitOfWork.Procedure<ProductImage>("pro_Get_Product_Images", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception)
            {
                totalRecord = 0;
                return new List<ProductImage>();
            }
        }

        public IList<ProductImage> GetByProductId(long productId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductID", productId);
                param.Add("@StatusIDActive", GlobalStatus.Active);                
                var values = unitOfWork.Procedure<ProductImage>("pro_Get_Product_Image_By_ProductID", param).ToList();                
                return values;
            }
            catch (Exception)
            {                
                return new List<ProductImage>();
            }
        } 

        public long Save(ProductImage obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userID);
                param.Add("@XML", XMLHelper.SerializeXML<ProductImage>(obj));
                param.Add("@ProductImageID", obj.ProductImageID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                var flag = unitOfWork.ProcedureExecute("pro_Update_Product_ImageInfo", param);
                return param.Get<long>("@ProductImageID");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        public long SaveUnicode(ProductImage obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userID);
                param.Add("@XML", XMLHelper.SerializeXML<ProductImage>(obj));
                param.Add("@ProductImageID", obj.ProductImageID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                param.Add("@FileName", obj.FileName);
                param.Add("@ImageUrl", obj.ImageUrl);
                var flag = unitOfWork.ProcedureExecute("pro_Update_Product_ImageInfo_Unicode", param);
                return param.Get<long>("@ProductImageID");
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
                return unitOfWork.ProcedureExecute("pro_Delete_Product_Image", new { ProductImageID = objId, UserID = userID, StatusIDDeleted = GlobalStatus.Deleted });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return false;
        }

        public bool DeleteByProductId(long productId, long userID)
        {
            try
            {
                return unitOfWork.ProcedureExecute("pro_Delete_Product_Image_By_ProductID", new { ProductID = productId, UserID = userID });
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
