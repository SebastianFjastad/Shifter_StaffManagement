using System;
using System.Globalization;

namespace System
{
    /// <summary>
    /// Additional extensions on <see cref="System.DateTime"/>
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns if the date is today's date
        /// </summary>
        public static bool IsToday(this DateTime value)
        {
            return (value.Date == DateTime.Today.Date);
        }

        /// <summary>
        /// Returns the start of the current week (From Monday not Sunday)
        /// </summary>
        public static DateTime StartOfWeek(this DateTime date)
        {
            var diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Returns the start of the month.
        /// </summary>
        public static DateTime StartOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        /// <summary>
        /// Returns the end of the month
        /// </summary>
        public static DateTime EndOfMonth(this DateTime value)
        {
            var start = value.StartOfMonth();
            return start.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Returns the week of year for the specified date
        /// </summary>
        public static int WeekOfYear(this DateTime date)
        {
            //return (int)Math.Ceiling((date - new DateTime(date.Year, 1, 1)).TotalDays / 7);
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
        
        /// <summary>
        /// Returns the week of year for the specified date with the specified start day
        /// </summary>
        public static int WeekOfYear(this DateTime date, DayOfWeek start)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, start);
        }
    }
}
