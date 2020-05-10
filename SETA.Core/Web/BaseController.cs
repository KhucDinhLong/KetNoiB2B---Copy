using System;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SETA.Core.SecurityServices.DataProviders;
using SETA.Core.Singleton;
using SETA.Entity;

namespace SETA.Core.Web
{
    //[NoCache]
    public class BaseController : Controller
    {
        public HttpContextBase HttpContextBase { get; set; }
        public BaseController()
        {            
        }

        public BaseController(HttpContextBase httpContextBase)
        {
            this.HttpContextBase = httpContextBase;
        }
        public Member CurrentUser
        {
            get
            {
                if (Session[Constant.TCZ_CURRENT_USER] == null)
                {
                    
                    var wd = System.Web.HttpContext.Current.User.Identity as FormsIdentity;

                    if (wd != null)
                    {
                        if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 && wd.Name.IndexOf(Constant.TCZ_MEMBER_SEPARATOR, StringComparison.Ordinal) > -1)
                        {
                            var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty),
                                 Constant.TCZ_MEMBER_SEPARATOR, RegexOptions.None);

                            if (authens.Length >= 2)
                            {
                                var ctx = SingletonIpl.GetInstance<DataProvider>();
                                Member userInfo = ctx.GetUser(authens[0]);
                                if (userInfo != null && userInfo.MemberID > 0)
                                {
                                    userInfo.ClientTime = authens.Length >= 3 ? authens[2] : string.Empty;
                                    userInfo.ClientDate = authens.Length >= 4 ? authens[3] : string.Empty;
                                    Session[Constant.TCZ_CURRENT_USER] = userInfo;
                                }                                
                            }
                        }
                        else if (wd.IsAuthenticated && wd.Name.IndexOf(Constant.TCZ_CURRENT_USER, StringComparison.Ordinal) > -1 && wd.Name.IndexOf(Constant.TCZ_ORIGIN_TEACHER, StringComparison.Ordinal) > -1)
                        {
                            var authens = Regex.Split(wd.Name.Replace(Constant.TCZ_CURRENT_USER, string.Empty), Constant.TCZ_ORIGIN_TEACHER, RegexOptions.None);

                            if (authens.Length >= 2)
                            {
                                var ctx = SingletonIpl.GetInstance<DataProvider>();
                                Member userInfo = ctx.GetUser(authens[0]);
                                if (userInfo != null)
                                {
                                    //userInfo.OriginTeacherUserName = authens[1];
                                    //userInfo.ClientTime = authens.Length >= 3 ? authens[2] : string.Empty;
                                    //userInfo.ClientDate = authens.Length >= 4 ? authens[3] : string.Empty;
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
                                Session[Constant.TCZ_CURRENT_USER] = userInfo;

                                if (!System.Web.HttpContext.Current.Request.IsSecureConnection && string.CompareOrdinal(Common.Utility.Utils.GetSetting(Common.Constants.AppKeys.ENABLE_REDIRECT_SSL, "0"), "1") == 0)
                                    Response.Redirect(System.Web.HttpContext.Current.Request.Url.ToString().Replace("http:", "https:"));
                            }
                        }
                    }                                                            

                    return (Member)Session[Constant.TCZ_CURRENT_USER];
                }
                else
                {
                    if (!System.Web.HttpContext.Current.Request.IsSecureConnection && string.CompareOrdinal(Common.Utility.Utils.GetSetting(Common.Constants.AppKeys.ENABLE_REDIRECT_SSL, "0"), "1") == 0)
                        Response.Redirect(System.Web.HttpContext.Current.Request.Url.ToString().Replace("http:", "https:"));
                    return (Member)Session[Constant.TCZ_CURRENT_USER];
                }
            }
            set
            {
                Session[Constant.TCZ_CURRENT_USER] = value;
            }
        }

        public string CurrentRoleViewMode
        {
            get
            {
                return Session[Constant.TCZ_VIEW_MODE] == null ? string.Empty : (string) Session[Constant.TCZ_VIEW_MODE];
            }
            set
            {
                Session[Constant.TCZ_VIEW_MODE] = value;
            }
        }
        
        /// <summary>
        /// Disable client cache
        /// </summary>
        protected void DisablePageCaching()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        //public sealed class NoCacheAttribute : ActionFilterAttribute
        //{
        //    public override void OnResultExecuting(ResultExecutingContext filterContext)
        //    {
        //        //filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        //        filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
        //        filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        //        filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        filterContext.HttpContext.Response.Cache.SetNoStore();

        //        base.OnResultExecuting(filterContext);
        //    }
        //}
    }
}
