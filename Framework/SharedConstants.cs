
namespace Framework
{
    /// <summary>
    /// General constants such as date formats that are used by many of the projects
    /// </summary>
    public static class SharedConstants
    {
        /// <summary>
        /// Date format, eg. "Sat 19 Apr 2014"
        /// </summary>
        public static string DateFormat = "ddd d MMM yyyy";

        /// <summary>
        /// Date format, eg. "Sat 19 Apr"
        /// </summary>
        public static string ShortDateFormatWithDay = "ddd d MMM";

        /// <summary>
        /// Date format, eg. "19 Apr"
        /// </summary>
        public static string ShortDateFormat = "d MMM";

        /// <summary>
        /// Date format, eg. "Sat 19"
        /// </summary>
        public static string ShortDayFormat = "ddd dd";

        /// <summary>
        /// Time format, eg. "02:57" (24 hour time for timespan but not datetime)
        /// </summary>
        public static string TimeFormat = "hh\\:mm";

        /// <summary>
        /// Time format, eg. "23:57" (24 hour for date time, will throw exception if used on timespan) 
        /// </summary>
        public static string DateTimeSpecificTimeFormat = "HH:mm";

        /// <summary>
        /// Notification type hint for shift deleted notification
        /// </summary>
        public static string DeleteNotification = "Delete";

        /// <summary>
        /// Notification type hint for shift assigned notification
        /// </summary>
        public static string AssignNotification = "Assign";
    }
}