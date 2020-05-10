using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using log4net.Repository.Hierarchy;
using SETA.Core.Helper.Logging;

namespace SETA.Core.Web
{
    /// <summary>
    /// Global error handler for application
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribute is AllowMultiple = true and users might want to override behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SetaHandleErrorAttribute : HandleErrorAttribute
    {
        public SetaHandleErrorAttribute()
        {
            //Logger.Instance.Info();
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.IsChildAction)
            {
                return;
            }



            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information. 
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            Exception exception = filterContext.Exception;

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method), 
            // ignore it.


            int httpCode = new HttpException(null, exception).GetHttpCode();
            if (httpCode != 500)
            {
                if (httpCode == 401)
                {
                    HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, "Error", "AccessDenied");
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "AccessDenied",
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
                    };
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 401;
                }

                if (httpCode == 404)
                {
                    HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, "Error", "NotFound");
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Not found",
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
                    };
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 401;
                }
                return;
            }



            if (!ExceptionType.IsInstanceOfType(exception))
            {
                return;
            }

            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            HandleErrorInfo model1 = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            filterContext.Result = new ViewResult
            {
                ViewName = View,
                MasterName = Master,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model1),
                TempData = filterContext.Controller.TempData
            };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            // Certain versions of IIS will sometimes use their own error page when 
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            //Logger.Instance.Error(string.Format("Application {0} has an error", Utils.GetSetting("AppName", "NA")), exception);
            Logging.PutError("Application has an error", exception);
        }        
    }
}
