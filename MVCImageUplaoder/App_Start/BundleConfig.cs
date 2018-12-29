using System.Web;
using System.Web.Optimization;

namespace MVCImageUplaoder
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts

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

            // Boorstrap file upload. 
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-file").Include(
                                  "~/Scripts/script-bootstrap-file.js"));

            #endregion

            #region Styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Bootstrap file upload.
            bundles.Add(new StyleBundle("~/Content/Bootstrap-file/css").Include(
                                 "~/Content/style/style-bootstrap-file.css"));

            #endregion
        }
    }
}
