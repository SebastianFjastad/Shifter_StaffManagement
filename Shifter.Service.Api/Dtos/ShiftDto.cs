using System;
using System.Runtime.Serialization;
using Framework;

namespace Shifter.Service.Api.Dtos
{
    [DataContract(IsReference = true)]
    public class ShiftDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public StaffDto Staff { get; set; }

        [DataMember]
        public RestaurantDto Restaurant { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public bool IsAvailable { get; set; }

        [DataMember]
        public bool IsCancelled { get; set; }

        public double NumDaysTillDueDate
        {
            get { return StartTime.Subtract(DateTime.Now).TotalDays; }
        }

        #region Methods

        /// <summary>
        /// Returns the waiters name who this shift is assigned to or "un-assigned" not assigned to a waiter.
        /// </summary>
        public string GetAssignedToHint()
        {
            var lable = "Available";

            if (Staff.IsNotNull())
            {
                lable = Staff.FirstName;
            }

            return lable;
        }

        /// <summary>
        /// Returns "Available" if available or empty string if not available.
        /// </summary>
        public string GetAvailabilityHint()
        {
            var lable = string.Empty;

            if (IsAvailable)
            {
                lable = "(Available)";
            }

            return lable;
        }

        /// <summary>
        /// Returns the waiters name who this shift is assigned to or "avalaible" if available.
        /// </summary>
        public string GetWaiterName()
        {
            return IsAvailable ? "Available" : Staff.FirstName;
        }

        /// <summary>
        /// Returns the start and end time formatted into a single string
        /// </summary>
        public string GetFormattedTime()
        {
            var startTime = StartTime.IsNotNull() ? StartTime.ToString(SharedConstants.DateTimeSpecificTimeFormat) : string.Empty;
            var endTime = EndTime.IsNotNull() ? EndTime.ToString(SharedConstants.DateTimeSpecificTimeFormat) : string.Empty;

            return string.Format("{0}-{1}", startTime, endTime);
        }

        public virtual bool IsTransient()
        {
            return Id <= 0;
        }

        #endregion
    }
}