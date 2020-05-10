using System.Web;
using System.Web.Optimization;

namespace KetNoiB2B
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/styles/LayoutCss").Include(
                "~/Content/animate.css",
                "~/Content/style.default.css",
                "~/Content/custom.css",
                "~/Content/Site.css"));

            //owl carousel css
            bundles.Add(new StyleBundle("~/styles/CarouselCss").Include(
                "~/Content/owl.carousel.css",
                "~/Content/owl.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/LayoutJs").Include(
                      "~/Scripts/jquery.cookie.js",
                      "~/Scripts/waypoints.min.js",
                      "~/Scripts/jquery.counterup.min.js",
                      "~/Scripts/jquery.parallax-1.1.3.js",
                      "~/Scripts/front.js"));

            //owl carousel
            bundles.Add(new ScriptBundle("~/bundles/CarouselJs").Include(
                      "~/Scripts/owl.carousel.min.js"));

            //gmapJs
            bundles.Add(new ScriptBundle("~/bundles/GmapJs").Include(
                "~/Scripts/gmap.js",
                "~/Scripts/gmap.init.js"));
        }
    }
}
