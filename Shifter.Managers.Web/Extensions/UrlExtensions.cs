using System.Web.Mvc;

namespace Shifter.Managers.Web.Extensions
{
    /// <summary>
    /// Url extensions
    /// </summary>
    public static class UrlExtensions
    {
        #region Extensions

        public static string PathToResetWaitersPassword(this UrlHelper url)
        {
            return url.Action("ResetPassword", "Waiter");
        }

        public static string PathToDeleteWaiter(this UrlHelper url)
        {
            return url.Action("DeleteWaiter", "Waiter");
        }

        public static string PathToAddTimeslot(this UrlHelper url)
        {
            return url.Action("AddTimeslot", "ShiftTimeslot");
        }
        
        public static string PathToRemoveTimeslot(this UrlHelper url)
        {
            return url.Action("DeleteTimeslot", "ShiftTimeslot");
        }

        public static string PathToLoadShifts(this UrlHelper url)
        {
            return url.Action("LoadShiftSchedule", "ShiftSchedule");
        }

        public static string PathToLoadShiftSummary(this UrlHelper url)
        {
            return url.Action("LoadShiftSummary", "ShiftSchedule");
        }  

        public static string PathToSaveShift(this UrlHelper url)
        {
            return url.Action("SaveShift", "ShiftSchedule");
        }

        public static string PathToSaveShiftChanges(this UrlHelper url)
        {
            return url.Action("SaveShiftChanges", "ShiftSchedule");
        }

        public static string PathToDeleteShift(this UrlHelper url)
        {
            return url.Action("DeleteShift", "ShiftSchedule");
        }

        public static string PathToFindMe(this UrlHelper url)
        {
            return url.Action("FindMe", "Account");
        }

        public static string PathToLoadWaiterHours(this UrlHelper url)
        {
            return url.Action("LoadWaiterHours", "Dashboard");
        }

        #endregion
    }
}