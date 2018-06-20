using LocalIntranet.Util;
using System.Web;
using System.Web.Optimization;

namespace LocalIntranet
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootbox.js"));
            /* ,
                      "~/Scripts/respond.js" */

            bundles.Add(new ScriptBundle("~/bundles/metisMenu").Include(
                      "~/Scripts/plugins/metisMenu/jquery.metisMenu.js"));

            /* cambiar rutas relativas dentro de los css 
            IItemTransform cssFixer = new CssRewriteUrlTransformWrapper();

            bundles.Add(new StyleBundle("~/Content/css/allcss")
                .Include("~/Content/css/bootstrap.css", cssFixer)
                .Include("~/Content/css/navbar-custom.css", cssFixer)
                .Include("~/Content/font-awesome/css/font-awesome.css", cssFixer)
                .Include("~/Content/css/sb-admin.css", cssFixer)
                .Include("~/Content/plugins/metisMenu/jquery.metisMenu.css", cssFixer));
            */

            /* Sin cambiar rutas, se ha agregado una carpeta 'fonts' dentro de Content */
            bundles.Add(new StyleBundle("~/Content/css/allcss").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/navbar-custom.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/css/sb-admin.css",
                "~/Content/plugins/metisMenu/jquery.metisMenu.css"));
            
        }
    }
}
