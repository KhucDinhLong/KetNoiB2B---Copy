using System;
using System.Web;
using System.Web.Security;
using SETA.Core.Security.Crypt;
using SETA.Core.Singleton;
using SETA.Core.Web;
using SETA.Entity;

namespace SETA.Core.SecurityServices.DataProviders
{
    public class DataProvider
    {
        /// <summary>
        /// Get user by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Member GetUser(string userName)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<SqlDataProvider>();
                return ctx.GetUser(userName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetUserByEmail(string email)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<SqlDataProvider>();
                return ctx.GetUserByEmail(email);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Validation UserName & Password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsAuthenticated(string userName, string password)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<SqlDataProvider>();

                return ctx.IsAuthenticated(userName, Md5Util.Md5EnCrypt(password));

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Validation Email
        /// </summary>
        /// <param name="email"></param>        
        /// <returns></returns>
        public bool IsAuthenticatedByEmail(string email)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<SqlDataProvider>();

                return ctx.IsAuthenticatedByEmail(email);

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Validation UserName
        /// </summary>
        /// <param name="userName"></param>        
        /// <returns></returns>
        public bool IsAuthenticatedByUserName(string userName)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<SqlDataProvider>();

                return ctx.IsAuthenticatedByUserName(userName);

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
