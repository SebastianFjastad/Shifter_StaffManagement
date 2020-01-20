using System;
using Framework.Domain;

namespace Shifter.Waiters.Domain.Models
{
    public class Shift : Entity
    {
        public Waiter Waiter { get; set; }

        public int RestaurantId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsCancelled { get; set; }

        #region Methods

        /// <summary>
        /// Returns the waiters name who this shift is assigned to or "avalaible" if available.
        /// </summary>
        public string GetWaiterName()
        {
            return IsAvailable ? "Available" : Waiter.FirstName;
        }

        /// <summary>
        /// Returns the start and end time formatted into a single string
        /// </summary>
        public string GetFormattedTime(string timeFormat)
        {
            var startTime = StartTime.IsNotNull() ? StartTime.Value.ToString(timeFormat) : string.Empty;
            var endTime = EndTime.IsNotNull() ? EndTime.Value.ToString(timeFormat) : string.Empty;

            return string.Format("{0} - {1}", startTime, endTime);
        }

        #endregion
    }
}
