using System;
using SETA.Core.SecurityServices.DataProviders;
using SETA.Core.Singleton;
using SETA.Entity;

namespace SETA.Core.SecurityServices.Utils
{
    public static class Authenticate
    {
        /// <summary>
        /// Authenticated
        /// </summary>
        /// <param name="username">AccountName</param>
        /// <param name="pwd">Password</param>
        /// <returns>true|false</returns>
        public static bool IsAuthenticated(string username, string pwd)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<DataProvider>();
                if (!ctx.IsAuthenticated(username, pwd))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Authenticated by Email
        /// </summary>
        /// <param name="email">email</param>        
        /// <returns>true|false</returns>
        public static bool IsAuthenticatedByEmail(string email)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<DataProvider>();
                if (!ctx.IsAuthenticatedByEmail(email))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Authenticated by UserName
        /// </summary>
        /// <param name="email">userName</param>        
        /// <returns>true|false</returns>
        public static bool IsAuthenticatedByUserName(string userName)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<DataProvider>();
                if (!ctx.IsAuthenticatedByUserName(userName))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
