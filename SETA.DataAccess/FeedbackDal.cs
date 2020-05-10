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
    public class FeedbackDal : BaseDal<ADOProvider>, IDataAccess<Feedback>
    {
        public Feedback Get(long id)
        {
            try
            {
                return unitOfWork.Procedure<Feedback>("mem_Get_FeedbackInfo",
                                                                 new
                                                                 {
                                                                     FeedbackID = id
                                                                 }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Feedback();
            }
        }

        public IList<Feedback> Get(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FirstName", listParam.Keyword);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var values = unitOfWork.Procedure<Feedback>("mem_Get_Feedbacks", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception)
            {
                totalRecord = 0;
                return new List<Feedback>();
            }
        }

        public long Save(Feedback obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userID);
                param.Add("@XML", XMLHelper.SerializeXML<Feedback>(obj));
                param.Add("@FeedbackID", obj.FeedbackID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                param.Add("@FirstName", obj.FirstName);
                param.Add("@LastName", obj.LastName);
                param.Add("@Subject", obj.Subject);
                param.Add("@Message", obj.Message);
                var flag = unitOfWork.ProcedureExecute("mem_Update_FeedbackInfo", param);
                return param.Get<long>("@FeedbackID");
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
                return unitOfWork.ProcedureExecute("mem_Delete_Feedback", new { FeedbackID = objId, UserID = userID, StatusIDDeleted = GlobalStatus.Deleted });
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
