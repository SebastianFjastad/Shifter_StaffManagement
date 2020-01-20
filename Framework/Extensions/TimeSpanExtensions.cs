using System;

namespace Framework.Extensions
{
    /// <summary>
    /// Additional extensions on <see cref="TimeSpan"/>
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns the time in a format hh:mm
        /// </summary>
        public static string HoursAndMinutes(this TimeSpan value)
        {
            Guard.ArgumentNotNull(value, "value");
            return value.ToString("hh\\:mm");
        }
    }
}
