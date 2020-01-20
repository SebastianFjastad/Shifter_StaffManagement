using System.Runtime.Serialization;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public class SettingsDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int RestaurantId { get; set; }

        /// <summary>
        /// Represents the number of days before the shift due date, that the manager would like to be notified if the shift is still unassigned.
        /// eg. If the value is 5 then the manager will be notified 5 days before the shift is due if it is not assigned.
        /// </summary>
        [DataMember]
        public int UnassignedShiftNotificationPeriod { get; set; }

        /// <summary>
        /// The set number of days before a shift is due that it becomes locked down for swapping
        /// eg. 2 days before the shifts due dates manager prohibits swapping
        /// </summary>
        [DataMember]
        public int NumDaysBeforeShiftSwappingLockDown { get; set; }
    }
}