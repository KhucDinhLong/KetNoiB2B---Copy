using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web;
using System.Web.Security;
using SETA.Common.Constants;
using SETA.Core.Helper.Mapping;
using SETA.Core.Helper.Session;
using SETA.Core.Security.Crypt;
using SETA.Core.SecurityServices.DataProviders;
using SETA.Core.SecurityServices.Utils;
using SETA.Core.Singleton;
using SETA.Core.Web;
using SETA.Entity;

namespace SETA.Core.SecurityServices
{
    public static class SecurityService
    {
        public static bool Login(string username, string password, bool rememberAccount)
        {                        
            Logout();
            if (Authenticate.IsAuthenticated(username, password))
            {
                FormsAuthentication.Initialize();                
                var timeoutTicket = Common.Utility.Utils.GetSetting<double>(AppKeys.TIMEOUT_REMEMBER_USER, 30);
                var curDay = DateTime.Now;

                var dateExpired = curDay.AddMinutes(HttpContext.Current.Session.Timeout);                

                if (rememberAccount)
                {
                    dateExpired = curDay.AddDays(timeoutTicket);                    
                }

                var ticket = new FormsAuthenticationTicket(1,
                    Constant.TCZ_CURRENT_USER + username + Constant.TCZ_MEMBER_SEPARATOR + password, DateTime.Now,
                    dateExpired, rememberAccount, "", FormsAuthentication.FormsCookiePath);

                string encrypetedTicket = FormsAuthentication.Encrypt(ticket);

                if (!FormsAuthentication.CookiesSupported)
                {
                    //If the authentication ticket is specified not to use cookie, set it in the URL
                    FormsAuthentication.SetAuthCookie(encrypetedTicket, false);
                }
                else
                {
                    //If the authentication ticket is specified to use a cookie,
                    //wrap it within a cookie.
                    //The default cookie name is .ASPXAUTH if not specified
                    //in the <forms> element in web.config
                    
                    //var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypetedTicket) { Expires = ticket.Expiration, HttpOnly = true};
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypetedTicket);
                    var timeoutRememberUser = Common.Utility.Utils.GetSetting<double>(AppKeys.TIMEOUT_REMEMBER_USER, 30);
                    if (rememberAccount)
                    {
                        authCookie.Expires = DateTime.Now.AddDays(timeoutRememberUser);                        
                    }

                    HttpCookie rememberCookie = new HttpCookie(AppKeys.COOKIE_REMEMBER);
                    rememberCookie["Remember"] = rememberAccount ? "1" : "0";
                    rememberCookie.Expires = DateTime.Now.AddDays(timeoutRememberUser);
                    HttpContext.Current.Response.Cookies.Add(rememberCookie);                    
                    //Set the cookie's expiration time to the tickets expiration time
                    //Set the cookie in the Response
                    HttpContext.Current.Response.Cookies.Add(authCookie);
                }                

                return true;
            }
            return false;
        }

        public static bool LoginByEmail(string email, string teacherEmail, bool rememberAccount)
        {
            HttpContext.Current.Session[Constant.TCZ_CURRENT_IS_TEACHER_FREE] = null;
            if (Authenticate.IsAuthenticatedByEmail(email))
            {
                Logout();                
                FormsAuthentication.Initialize();

                var ticket = new FormsAuthenticationTicket(1, Constant.TCZ_CURRENT_EMAIL + email + Constant.TCZ_ORIGIN_TEACHER + teacherEmail, DateTime.Now,
                    DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout), rememberAccount, "",
                    FormsAuthentication.FormsCookiePath);

                string encrypetedTicket = FormsAuthentication.Encrypt(ticket);

                if (!FormsAuthentication.CookiesSupported)
                {                    
                    FormsAuthentication.SetAuthCookie(encrypetedTicket, false);
                }
                else
                {                    
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypetedTicket);
                    if (rememberAccount)
                    {
                        authCookie.Expires = ticket.Expiration;
                    }
                    
                    HttpContext.Current.Response.Cookies.Add(authCookie);
                }

                return true;
            }
            return false;
        }

        public static bool LoginByUserName(string userName, string teacherUserName, bool rememberAccount, string clientTime = "", string clientDate = "")
        {
            HttpContext.Current.Session[Constant.TCZ_CURRENT_IS_TEACHER_FREE] = null;
            if (Authenticate.IsAuthenticatedByUserName(userName))
            {
                //Logout();
                ClearSessionMessageUnread();
                var timeout = Common.Utility.Utils.GetSetting<double>(Common.Constants.AppKeys.TIMEOUT_LOGIN_AS, 30);
                FormsAuthentication.Initialize();

                var ticket = new FormsAuthenticationTicket(1, Constant.TCZ_CURRENT_USER + userName + Constant.TCZ_ORIGIN_TEACHER + teacherUserName + Constant.TCZ_CURRENT_TIMEZONE + clientTime + "|" + clientDate + "|" + DateTime.Now.AddMinutes(timeout).ToString("G"), DateTime.Now,
                    DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout), rememberAccount, "",
                    FormsAuthentication.FormsCookiePath);

                string encrypetedTicket = FormsAuthentication.Encrypt(ticket);

                if (!FormsAuthentication.CookiesSupported)
                {
                    FormsAuthentication.SetAuthCookie(encrypetedTicket, false);
                }
                else
                {
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypetedTicket);
                    if (rememberAccount)
                    {
                        authCookie.Expires = ticket.Expiration;
                    }

                    HttpContext.Current.Response.Cookies.Add(authCookie);
                }

                return true;
            }
            return false;
        }

        public static void Logout()
        {
            // Logout
            FormsAuthentication.SignOut();            
            //Clear session            
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();            
            
            //Clears out Session
            HttpContext.Current.Response.Cookies.Clear();

            // clear authentication cookie
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }            
        }

        public static void ClearSessionMessageUnread()
        {
            HttpContext.Current.Session[Constant.TCZ_CURRENT_UNREAD] = null;
        }
    }
}
