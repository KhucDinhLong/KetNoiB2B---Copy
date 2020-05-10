using System;
using System.Data;
using System.Linq;
using Dapper;
using SETA.Core.Base;
using SETA.Core.Data;
using SETA.Entity;

namespace SETA.Core.SecurityServices.DataProviders
{
    public class SqlDataProvider : BaseDal<ADOProvider>
    {
        /// <summary>
        /// Get user by UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Member GetUser(string userName)
        {
            try
            {
                var user = unitOfWork.Procedure<Member>("mem_Get_UserByUserName", new { UserName = userName }).FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get user by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetUserByEmail(string email)
        {
            try
            {
                var user = unitOfWork.Procedure<Member>("mem_Get_UserByEmail", new { Email = email }).FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Check Authenticated
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsAuthenticated(string userName, string password)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", userName);
                param.Add("@PassWord", password);
                param.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                unitOfWork.Procedure<Member>("mem_Get_UserAuthenticated", param);
                return param.GetDataOutput<bool>("@Result");
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check Authenticated by Email
        /// </summary>
        /// <param name="email"></param>        
        /// <returns></returns>
        public bool IsAuthenticatedByEmail(string email)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Email", email);                
                param.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                unitOfWork.Procedure<Member>("mem_Get_UserAuthenticated_ByEmail", param);
                return param.GetDataOutput<bool>("@Result");
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check Authenticated by UserName
        /// </summary>
        /// <param name="userName"></param>        
        /// <returns></returns>
        public bool IsAuthenticatedByUserName(string userName)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", userName);
                param.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                unitOfWork.Procedure<Member>("mem_Get_UserAuthenticated_ByUserName", param);
                return param.GetDataOutput<bool>("@Result");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
