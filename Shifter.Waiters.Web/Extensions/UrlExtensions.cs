using System.Web.Mvc;

namespace Shifter.Waiters.Web.Extensions
{
    /// <summary>
    /// Url extensions
    /// </summary>
    public static class UrlExtensions
    {
        #region Extensions

        public static string PathToLoadShiftsToTake(this UrlHelper url)
        {
            return url.Action("LoadShiftsToTake", "ShiftSchedule");
        }

        public static string PathToLoadShiftsToUpdate(this UrlHelper url)
        {
            return url.Action("LoadShiftsToUpdate", "ShiftSchedule");
        }
        
        public static string PathToLoadShiftSchedule(this UrlHelper url)
        {
            return url.Action("LoadAvailableShiftSchedule", "ShiftSchedule");
        }

        public static string PathToLoadAllShiftsSchedule(this UrlHelper url)
        {
            return url.Action("LoadAllShiftsSchedule", "ShiftSchedule");
        }

        public static string PathToTakeShift(this UrlHelper url)
        {
            return url.Action("TakeShift", "ShiftSchedule");
        }

        public static string PathToUpdateShiftAvailability(this UrlHelper url)
        {
            return url.Action("UpdateShiftAvailablity", "ShiftSchedule");
        }


        public static string PathToFindMe(this UrlHelper url)
        {
            return url.Action("FindMe", "Account");
        }
        
        #endregion
    }
}