using System.Web.Optimization;

namespace Shifter.Waiters.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").IncludeDirectory("~/Content/bootstrap/", "*.css", searchSubdirectories: true));

            bundles.Add(new StyleBundle("~/Content/Custom/css").IncludeDirectory("~/Content/Custom/", "*.css", searchSubdirectories: true));

            bundles.Add(new StyleBundle("~/Content/jquery/css").Include("~/Content/jquery/jquery-ui-1.10.4.custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/loginAppLibs").Include(
                "~/Scripts/App/Account/ResetPassword.js",
                "~/Scripts/App/DataService.js"));

            bundles.Add(new ScriptBundle("~/bundles/appLibs").IncludeDirectory("~/Scripts/App/", "*.js", searchSubdirectories: true));

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                "~/Scripts/Lib/jquery-1.11.0.js",
                "~/Scripts/Lib/jquery-ui-1.10.4.custom.js",
                "~/Scripts/Lib/jquery.signalR-2.0.0.js",
                //"~/Scripts/Lib/jquery.validate.js", seems to be causing a bug on IE 
                //"~/Scripts/Lib/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/Lib/confirm-bootstrap.js",
                "~/Scripts/Lib/amplify.js",
                "~/Scripts/Lib/notify.js",
                "~/Scripts/Lib/bootstrap.js",
                "~/Scripts/Lib/bootstrap-datepicker.js"));
        }
    }
}