using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SETA.Core.Web
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SetaAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public SetaAuthorizeAttribute(params string[] roles): base()
        {
            _roles = string.Join(",", roles);
        }

        private readonly object _typeId = new object();

        private string _groups;
        private string[] _groupsSplit = new string[0];

        private string _roles;
        private string[] _rolesSplit = new string[0];

        private string _allowmodules;
        private string[] _allowmodulesSplit = new string[0];

        private string _users;
        private string[] _usersSplit = new string[0];

        public string Roles
        {
            get { return _roles ?? String.Empty; }
            set
            {
                _roles = value;
                _rolesSplit = SplitString(value);
            }
        }

        public override object TypeId
        {
            get { return _typeId; }
        }

        public string Users
        {
            get { return _users ?? String.Empty; }
            set
            {
                _users = value;
                _usersSplit = SplitString(value);
            }
        }

        public string Groups
        {
            get { return _groups ?? String.Empty; }
            set
            {
                _groups = value;
                _groupsSplit = SplitString(value);
            }
        }

        public string AllowModules
        {
            get { return _allowmodules ?? String.Empty; }
            set
            {
                _allowmodules = value;
                _allowmodulesSplit = SplitString(value);
            }
        }

        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
           

            var user = httpContext.Session.CurrentUser();

            //// Check User is Authorize in Group
            //if (_groupsSplit.Length > 0 && !_groupsSplit.Any(user.IsInGroup))
            //{
            //    return false;
            //}

            // Check User is Authorize in Role 
            if (_rolesSplit.Length > 0 && !_rolesSplit.Any(user.IsInRole))
            {
                return false;
            }

            //// Check User is Authorize in Module 
            //if (_allowmodulesSplit.Length > 0 && !_allowmodulesSplit.Any(user.IsInAllowModule))
            //{
            //    return false;
            //}

            // Check User is Authorize CurrentUser
            if (_usersSplit.Length > 0 && !_usersSplit.Contains(user.UserName, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                // If a child action cache block is active, we need to fail immediately, even if authorization
                // would have succeeded. The reason is that there's no way to hook a callback to rerun
                // authorization before the fragment is served from the cache, so we can't guarantee that this
                // filter will be re-run on subsequent requests.
                throw new InvalidOperationException("MvcResources.AuthorizeAttribute_CannotUseWithinChildActionCache");
            }

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(
                                      typeof(AllowAnonymousAttribute), inherit: true)
                                     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                                         typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            // Check User Login
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //Fixed error repeart redirect SuNV 2016-10-20
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                //End.
                filterContext.Result = new RedirectToRouteResult("Default",
                new RouteValueDictionary(new { controller = "Home", action = "Index", re = HttpContext.Current.Request.Url.PathAndQuery }));
                return;
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Unauthorized
            // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
            // filterContext.Result = new HttpNotFoundResult();

            string guid = Guid.NewGuid().ToString();
            filterContext.Result = new RedirectToRouteResult("Default",
                new RouteValueDictionary(new { controller = "Error", action = "NotFound"}));
        }

        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }

        internal static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
    }
}
