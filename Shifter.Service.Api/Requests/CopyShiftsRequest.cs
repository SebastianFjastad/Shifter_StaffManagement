using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class CopyShiftsRequest
    {
        [DataMember]
        public DateTime CopyFromStartDate { get; set; }

        [DataMember]
        public DateTime CopyFromEndDate { get; set; }

        [DataMember]
        public DateTime CopyToStartDate { get; set; }

        [DataMember]
        public int RestaurantId { get; set; } 
        
        [DataMember]
        public IEnumerable<int> StaffTypeIds { get; set; }

        [DataMember]
        public bool OverwriteExistingShifts { get; set; }
    }
}