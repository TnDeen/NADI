using System.Web;
using System.Web.Optimization;

namespace MVC5
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
                      "~/Scripts/respond.js",
                      "~/Content/assets/global/plugins/jquery.min.js",
                      "~/Content/assets/global/plugins/jquery-migrate.min.js",
                      "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Content/assets/frontend/layout/scripts/back-to-top.js",
                      "~/Content/assets/global/plugins/fancybox/source/jquery.fancybox.pack.js",
                      "~/Content/assets/global/plugins/carousel-owl-carousel/owl-carousel/owl.carousel.min.js",
                      "~/Content/assets/global/plugins/slider-revolution-slider/rs-plugin/js/jquery.themepunch.revolution.min.js",
                      "~/Content/assets/global/plugins/slider-revolution-slider/rs-plugin/js/jquery.themepunch.tools.min.js",
                      "~/Content/assets/frontend/pages/scripts/revo-slider-init.js",
                      "~/Content/assets/frontend/layout/scripts/layout.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/fancybox/source/jquery.fancybox.css",
                      "~/Content/assets/global/plugins/carousel-owl-carousel/owl-carousel/owl.carousel.css",
                      "~/Content/assets/global/plugins/slider-revolution-slider/rs-plugin/css/settings.css",
                      "~/Content/assets/global/css/components.css",
                      "~/Content/assets/frontend/layout/css/style.css",
                      "~/Content/assets/frontend/pages/css/style-revolution-slider.css",
                      "~/Content/assets/frontend/layout/css/style-responsive.css",
                      "~/Content/assets/frontend/layout/css/themes/red.css",
                      "~/Content/assets/frontend/layout/css/custom.css"));
        }
    }
}
