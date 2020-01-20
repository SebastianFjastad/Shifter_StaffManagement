using System;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public class ShiftTimeSlotDto
    {
        #region Properties

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public TimeSpan StartTime { get; set; }

        [DataMember]
        public TimeSpan EndTime { get; set; }

        [DataMember]
        public int StaffTypeId { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the start and end time formatted into a single string
        /// </summary>
        public string GetFormattedTime(string timeFormat)
        {
            var startTime = StartTime.IsNotNull() ? StartTime.ToString(timeFormat) : string.Empty;
            var endTime = EndTime.IsNotNull() ? EndTime.ToString(timeFormat) : string.Empty;

            return string.Format("{0}-{1}", startTime, endTime);
        }

        #endregion
    }
}