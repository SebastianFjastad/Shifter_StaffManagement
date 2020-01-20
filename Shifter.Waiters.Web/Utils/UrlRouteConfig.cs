using System.Web;
using System.Web.Mvc;
using Shifter.Waiters.Web.Extensions;

namespace Shifter.Waiters.Web.Utils
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

            SetupShiftScheduleRoutes(urlHelper);
            SetupAccountRoutes(urlHelper);
        }

        #endregion

        #region Properties

        public object ShiftSchedule { get; set; }
        public object Account { get; set; }

        #endregion

        #region Private methods

        private void SetupShiftScheduleRoutes(UrlHelper url)
        {
            ShiftSchedule = new
            {
                pathToLoadShiftsToTake = url.PathToLoadShiftsToTake(),
                pathToLoadShiftsToUpdate = url.PathToLoadShiftsToUpdate(),
                pathToLoadShiftSchedule = url.PathToLoadShiftSchedule(),
                pathToLoadAllShiftsSchedule = url.PathToLoadAllShiftsSchedule(),
                pathToTakeShift = url.PathToTakeShift(),
                pathToUpdateShiftAvailability = url.PathToUpdateShiftAvailability(),
            };
        }

        private void SetupAccountRoutes(UrlHelper url)
        {
            Account = new
            {
                pathToFindMe = url.PathToFindMe(),
            };
        }

        #endregion
    }
}