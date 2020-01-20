using System.Web.Optimization;

namespace Shifter.Managers.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap/css").IncludeDirectory("~/Content/bootstrap/", "*.css", searchSubdirectories: true));
            bundles.Add(new StyleBundle("~/Content/Other/css").IncludeDirectory("~/Content/Other/", "*.css", searchSubdirectories: true));
            bundles.Add(new StyleBundle("~/Content/FontAwsome/css").IncludeDirectory("~/Content/FontAwsome/", "font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/ShiftSchedule/css").IncludeDirectory("~/Content/CustomCss/ShiftSchedule", "*.css", searchSubdirectories: true));


            bundles.Add(new ScriptBundle("~/bundles/appLibs").IncludeDirectory("~/Scripts/App/", "*.js", searchSubdirectories: true));

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                "~/Scripts/Lib/jquery-1.11.0.js",
                "~/Scripts/Lib/jquery.signalR-2.0.0.js",
                "~/Scripts/Lib/jquery.validate.js",
                "~/Scripts/Lib/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/Lib/confirm-bootstrap.js",
                "~/Scripts/Lib/bootstrap-timepicker.js",
                "~/Scripts/Lib/amplify.js",
                "~/Scripts/Lib/bootstrap.js",
                "~/Scripts/Lib/bootstrap-datepicker.js",
                "~/Scripts/Lib/jquery.joyride-2.1.js",
                "~/Scripts/Lib/bootstrap-multiselect.js",
                "~/Scripts/Lib/tinymce.js"
                ));

        }
    }
}