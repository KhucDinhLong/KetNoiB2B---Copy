using System.Web.Mvc;

namespace SETA.Core.Helper.Extensions
{
    public static class UrlHelperExtension
    {
        public static string Home(this UrlHelper helper)
        {
            return helper.Content("~/");
        }
    }
}
