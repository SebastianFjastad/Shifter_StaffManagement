using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadShiftsRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public int? TimeSlotId { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public DateTime? ToDate { get; set; }

        [DataMember]
        public bool? IsAssigned { get; set; }

        [DataMember]
        public bool? IsAvailable { get; set; }

        [DataMember]
        public IEnumerable<int> StaffTypeIds { get; set; }

        [DataMember]
        public int? StaffId { get; set; }
    }
}