using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadStaffHoursRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public IEnumerable<int> StaffTypeIds { get; set; }
    }
}