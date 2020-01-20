using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadStaffListRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public DateTime? IncludeShiftsFrom { get; set; }

        [DataMember]
        public DateTime? IncludeShiftsTo { get; set; }

        [DataMember]
        public IEnumerable<int> StaffTypeIds { get; set; }
    }
}