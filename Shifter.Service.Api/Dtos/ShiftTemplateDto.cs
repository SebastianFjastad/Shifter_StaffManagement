using System;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public class ShiftTemplateDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DayOfWeek DayOfWeek { get; set; }

        [DataMember]
        public ShiftTimeSlotDto TimeSlot { get; set; }

        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public int NumberOfWaitersNeeded { get; set; }
    }
}