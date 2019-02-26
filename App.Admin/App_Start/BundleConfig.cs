using System.Web;
using System.Web.Optimization;

namespace App.Admin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //  Bundles do Admin Theme

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/font-face.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/pixel-admin.min.css",
                      "~/Content/css/widgets.min.css",
                      "~/Content/css/rtl.min.css",
                      "~/Content/css/themes.min.css",
                      "~/Content/css/_Style.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/library.not.ie").Include(
                        "~/Scripts/jquery.min.2.0.3.js",
                        "~/Scripts/jquery.maskedinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/library.only.ie").Include(
                        "~/Scripts/jquery.min.1.8.3.ie9back.js",
                        "~/Scripts/jquery.maskedinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/jquery-form-plugins.js",
                        "~/Scripts/jquery-form-ready.js",
                        "~/Scripts/pixel-admin.min.js",
                        "~/Scripts/clipboard.min.js",
                        "~/Scripts/_Admin.js"
                        ));
        }
    }
}
