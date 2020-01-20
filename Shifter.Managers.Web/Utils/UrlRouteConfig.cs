using System.Web;
using System.Web.Mvc;
using Shifter.Managers.Web.Extensions;

namespace Shifter.Managers.Web.Utils
{
    /// <summary>
    /// Sets up paths to be used in javaScript
    /// </summary>
    public class UrlRouteConfig
    {
        #region Constructors

        public UrlRouteConfig()
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            SetupWaiterRoutes(urlHelper);
            SetupTimeslotRoutes(urlHelper);
            SetupShiftRoutes(urlHelper);
            SetupAccountRoutes(urlHelper);
            SetupSettingRoutes(urlHelper);
            SetupWallRoutes(urlHelper);
        }

        #endregion

        #region Properties

        public object Waiter { get; set; }

        public object Timeslot { get; set; }

        public object Shift { get; set; }

        public object Account { get; set; }

        public object Settings { get; set; }

        public object Wall { get; set; }

        #endregion

        #region Private methods

        private void SetupWaiterRoutes(UrlHelper url)
        {
            Waiter = new
            {
                pathToDeleteWaiter = url.PathToDeleteWaiter(),
                pathToResetWaitersPassword = url.PathToResetWaitersPassword(),
            };
        }

        private void SetupTimeslotRoutes(UrlHelper url)
        {
            Timeslot = new
            {
                pathToAddTimeslot = url.PathToAddTimeslot(),
                pathToRemoveTimeslot = url.PathToRemoveTimeslot(),
            };
        }

        private void SetupWallRoutes(UrlHelper url)
        {
            Wall = new
            {
                pathToNewPost = url.Action("NewPost", "Wall"),
                pathToLoadPost = url.Action("LoadPost", "Wall"),
                pathToLoadPosts = url.Action("LoadWallPosts", "Wall"),
            };
        }

        private void SetupShiftRoutes(UrlHelper url)
        {
            Shift = new
            {
                pathToLoadShifts = url.PathToLoadShifts(),
                pathToLoadShiftSummary = url.PathToLoadShiftSummary(),
                pathToSaveShift = url.PathToSaveShift(),
                pathToDeleteShift = url.PathToDeleteShift(),
                pathToCopySchedule = url.Action("CopySchedule", "ShiftSchedule"),
                pathToForceScheduleCopy = url.Action("ForceScheduleCopy", "ShiftSchedule"),
                pathToEditShifts = url.Action("EditShifts", "ShiftSchedule"),
                pathToSaveShiftChanges = url.Action("SaveShiftChanges", "ShiftSchedule"),
            };
        }

        private void SetupAccountRoutes(UrlHelper url)
        {
            Account = new
            {
                pathToFindMe = url.PathToFindMe(),
            };
        }
        
        private void SetupSettingRoutes(UrlHelper url)
        {
            Settings = new
            {
                pathToLoadWaiterHours = url.PathToLoadWaiterHours(),
            };
        }

        #endregion
    }
}