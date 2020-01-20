using System;
using Framework.Extensions;
using Shifter.Waiters.Web.Utils;

namespace Shifter.Waiters.Web.ViewModels
{
    public class ShiftScheduleViewModel
    {
        #region Methods

        /// <summary>
        /// Returns a formatted date string based on the day and week(1,2,3 or 4) provided
        /// </summary>
        public string GetDateFromDayAndWeek(int day, int week, string format = null)
        {
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

            format = format.IsNotNullOrEmpty() ? format : Constants.ShortDateFormat;

            return DateTime.Now.StartOfWeek().AddDays(day).ToString(format);
        }

        /// <summary>
        /// Returns a formatted string of the from and to date of the provided week
        /// </summary>
        public string GetDateFromTo(int week)
        {
            var dateFrom = GetDateFromDayAndWeek((int)DayOfWeek.Sunday, week, Constants.ShortDateFormat);
            var dateTo = GetDateFromDayAndWeek((int)DayOfWeek.Monday, week, Constants.ShortDateFormat);

            return string.Format("{0} - {1}", dateFrom, dateTo);
        }

        #endregion
    }
}