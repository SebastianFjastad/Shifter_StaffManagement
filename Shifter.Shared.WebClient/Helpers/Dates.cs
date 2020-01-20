using System;
using Framework;
using Framework.CustomTypes;

namespace Shifter.Shared.WebClient.Helpers
{
    public class Dates
    {
        /// <summary>
        /// Returns a formatted date string based on the day and week(1,2,3 or 4) provided
        /// </summary>
        public static string GetFormattedDateFromDayAndWeek(int day, int week, string format = null)
        {
            var date = GetDateFromDayAndWeek(day, week);

            format = format.IsNotNullOrEmpty() ? format : SharedConstants.ShortDateFormat;

            return date.ToString(format);
        }

        public static DateTime GetDateFromDayAndWeek(int day, int week, DateTime? startWeekDate = null)
        {
            if (startWeekDate.IsNull())
            {
                startWeekDate = DateTime.Now.StartOfWeek();
            }

            day = day - 1;

            if (week == 2)
            {
                day = day + 7;
            }
            if (week == 3)
            {
                day = day + 14;
            }
            if (week == 4)
            {
                day = day + 21;
            }

            return startWeekDate.Value.AddDays(day);
        }

        /// <summary>
        /// Returns a formatted string of the from and to date of the provided week
        /// </summary>
        public static string GetDateFromTo(int week, DateTime? startWeekDate = null)
        {
            if (startWeekDate.IsNull())
            {
                startWeekDate = DateTime.Now.StartOfWeek();
            }

            var dateFrom = GetDateFromDayAndWeek((int)DayOfWeekStartingAtMonday.Monday, week, startWeekDate).ToString(SharedConstants.ShortDateFormat);
            var dateTo = GetDateFromDayAndWeek((int)DayOfWeekStartingAtMonday.Sunday, week, startWeekDate).ToString(SharedConstants.ShortDateFormat);

            return String.Format("{0}-{1}", dateFrom, dateTo);
        }
    }
}