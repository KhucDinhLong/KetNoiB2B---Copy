using System;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using SETA.Core.SecurityServices.DataProviders;
using SETA.Core.Singleton;
using SETA.Entity;

namespace SETA.Core.Web
{
    /// <summary>
    /// Web extension 
    /// </summary>
    public static class WebExtension
    {
        /// <summary>
        /// Get current user info from session
        /// </summary>
        /// <param name="session">Http Session</param>
        /// <returns>User Info class</returns>
        public static Member CurrentUser(this HttpSessionStateBase session)
        {
            var wd = HttpContext.Current.User.Identity;
            //var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //var ticketInfo = FormsAuthentication.Decrypt(cookie.Value);

            if (wd == null || wd.Name == null)
                return null;


            if (session[Constant.TCZ_CURRENT_USER] == null)
            {
                var ctx = SingletonIpl.GetInstance<DataProvider>();
                if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 && wd.Name.IndexOf(Constant.TCZ_MEMBER_SEPARATOR, StringComparison.Ordinal) > -1)
                {                    
                    var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty),
                        Constant.TCZ_MEMBER_SEPARATOR, RegexOptions.None);

                    if (authens.Length >= 2)
                    {
                        //!TODO...
                        Member userInfo = ctx.GetUser(authens[0]);
                        if (userInfo != null && userInfo.MemberID > 0)
                        {
                            userInfo.ClientTime = authens.Length >= 3 ? authens[2] : string.Empty;
                            userInfo.ClientDate = authens.Length >= 4 ? authens[3] : string.Empty;
                            session[Constant.TCZ_CURRENT_USER] = userInfo;
                            HttpContext.Current.Items[Constant.TCZ_CURRENT_USER] = userInfo;
                        }                        
                        return userInfo;
                    }
                }
                else if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 && wd.Name.IndexOf(Constant.TCZ_ORIGIN_TEACHER, StringComparison.Ordinal) > -1)
                {
                    var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty), Constant.TCZ_ORIGIN_TEACHER, RegexOptions.None);                    
                    if (authens.Length >= 2)
                    {
                        var userInfo = ctx.GetUser(authens[0]);
                        if (userInfo != null)
                        {                                                        
                            var arrTimezone = Regex.Split(authens[1], Constant.TCZ_CURRENT_TIMEZONE, RegexOptions.None);
                            if (arrTimezone.Length >= 2)
                            {
                                userInfo.OriginTeacherUserName = arrTimezone[0];
                                var arrTime = arrTimezone[1].Split('|');
                                if (arrTime.Length >= 2)
                                {
                                    userInfo.ClientTime = arrTime[0];
                                    userInfo.ClientDate = arrTime[1];
                                    if (arrTime.Length >= 3)
                                    {
                                        var dateTimeOut = DateTime.Parse(arrTime[2]);
                                        if (DateTime.Now > dateTimeOut)
                                        {
                                            SecurityServices.SecurityService.LoginByUserName(
                                                userInfo.OriginTeacherUserName, userInfo.OriginTeacherUserName, true,
                                                userInfo.ClientTime, userInfo.ClientDate);
                                        }
                                    }
                                }
                            }                                                     
                        }
                        session[Constant.TCZ_CURRENT_USER] = userInfo;
                        HttpContext.Current.Items[Constant.TCZ_CURRENT_USER] = userInfo;
                        return userInfo;
                    }
                }
            }
            else
            {
                var ctx = SingletonIpl.GetInstance<DataProvider>();
                var userInfo = (Member)session[Constant.TCZ_CURRENT_USER];                
                if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 &&
                    wd.Name.IndexOf(Constant.TCZ_MEMBER_SEPARATOR, StringComparison.Ordinal) > -1)
                {
                    var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty),
                       Constant.TCZ_MEMBER_SEPARATOR, RegexOptions.None);

                    if (authens.Length >= 2 && !userInfo.UserName.Equals(authens[0]))
                    {                                                
                        //!TODO...
                        userInfo = ctx.GetUser(authens[0]);
                        userInfo.ClientTime = authens.Length >= 3 ? authens[2] : string.Empty;
                        userInfo.ClientDate = authens.Length >= 4 ? authens[3] : string.Empty;
                        session[Constant.TCZ_CURRENT_USER] = userInfo;
                    }
                }
                else if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 && wd.Name.IndexOf(Constant.TCZ_ORIGIN_TEACHER, StringComparison.Ordinal) > -1)
                {
                    var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty), Constant.TCZ_ORIGIN_TEACHER, RegexOptions.None);
                    if (authens.Length >= 2 && !String.IsNullOrEmpty(userInfo.UserName) && !userInfo.UserName.Equals(authens[0]))
                    {
                        userInfo = ctx.GetUser(authens[0]);
                        if (userInfo != null)
                        {
                            var arrTimezone = Regex.Split(authens[1], Constant.TCZ_CURRENT_TIMEZONE, RegexOptions.None);
                            if (arrTimezone.Length >= 2)
                            {
                                userInfo.OriginTeacherUserName = arrTimezone[0];
                                var arrTime = arrTimezone[1].Split('|');
                                if (arrTime.Length >= 2)
                                {
                                    userInfo.ClientTime = arrTime[0];
                                    userInfo.ClientDate = arrTime[1];
                                    if (arrTime.Length >= 3)
                                    {
                                        var dateTimeOut = DateTime.Parse(arrTime[2]);
                                        if (DateTime.Now > dateTimeOut)
                                        {
                                            SecurityServices.SecurityService.LoginByUserName(
                                                userInfo.OriginTeacherUserName, userInfo.OriginTeacherUserName, true,
                                                userInfo.ClientTime, userInfo.ClientDate);
                                        }
                                    }
                                }
                            }
                        }
                        session[Constant.TCZ_CURRENT_USER] = userInfo;                        
                    }
                }

            }

            return session[Constant.TCZ_CURRENT_USER] as Member;
        }

        /// <summary>
        /// Get current view mode from session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string CurrentViewMode(this HttpSessionStateBase session)
        {
            if (session[Constant.TCZ_VIEW_MODE] == null)
            {
                return string.Empty;
            }
            return session[Constant.TCZ_VIEW_MODE] as string;
        }

        /// <summary>
        /// Check is viewmode
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsViewMode(this HttpSessionStateBase session)
        {
            return (session[Constant.TCZ_VIEW_MODE] != null);
        }

        /// <summary>
        /// Check user is authenticate
        /// </summary>
        /// <param name="session">Http Session</param>
        /// <returns>True if user is authenticate</returns>
        public static bool IsAuthenticate(this HttpSessionStateBase session)
        {
            var wd = HttpContext.Current.User.Identity as WindowsIdentity;

            return (wd != null && wd.User != null);
        }

        /// <summary>
        /// Get current user info from session
        /// </summary>
        /// <param name="session">Http Session</param>
        /// <returns>User Info class</returns>
        public static Member CurrentUser(this HttpSessionState session)
        {
            var wd = HttpContext.Current.User.Identity;

            if (wd == null || wd.Name == null)
                return null;


            if (session[Constant.TCZ_CURRENT_USER] == null)
            {
                if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 && wd.Name.IndexOf(Constant.TCZ_MEMBER_SEPARATOR, StringComparison.Ordinal) > -1)
                {
                    var ctx = SingletonIpl.GetInstance<DataProvider>();
                    var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty),
                        Constant.TCZ_MEMBER_SEPARATOR, RegexOptions.None);

                    if (authens.Length >= 2)
                    {
                        //Member userInfo = ctx.GetUser(authens[0], authens[1]);
                        
                        //TODO
                        Member userInfo = ctx.GetUser(authens[0]);

                        session[Constant.TCZ_CURRENT_USER] = userInfo;
                        HttpContext.Current.Items[Constant.TCZ_CURRENT_USER] = userInfo;
                        return userInfo;
                    }
                }
            }
            else
            {
                var userInfo = (Member)session[Constant.TCZ_CURRENT_USER];
                if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 &&
                    wd.Name.IndexOf(Constant.TCZ_MEMBER_SEPARATOR, StringComparison.Ordinal) > -1)
                {
                    var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty),
                       Constant.TCZ_MEMBER_SEPARATOR, RegexOptions.None);

                    if (authens.Length >= 2 && !userInfo.UserName.Equals(authens[0]))
                    {
                        //userInfo = UserService.GetBySId(wd.User.ToString());
                        var ctx = SingletonIpl.GetInstance<DataProvider>();
                        userInfo = ctx.GetUser(authens[0]);
                        session[Constant.TCZ_CURRENT_USER] = userInfo;
                    }
                }

            }

            return session[Constant.TCZ_CURRENT_USER] as Member;
        }

        /// <summary>
        /// Check user is authenticate
        /// </summary>
        /// <param name="session">Http Session</param>
        /// <returns>True if user is authenticate</returns>
        public static bool IsAuthenticate(this HttpSessionState session)
        {
            var wd = HttpContext.Current.User.Identity as WindowsIdentity;

            return (wd != null && wd.User != null);
        }

    }
}
