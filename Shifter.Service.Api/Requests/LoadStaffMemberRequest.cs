using System;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadStaffMemberRequest
    {
        [DataMember]
        public int StaffId { get; set; }

        [DataMember]
        public int? RestaurantId { get; set; }

        [DataMember]
        public DateTime? IncludeShiftsTo { get; set; }

        [DataMember]
        public DateTime? IncludeShiftsFrom { get; set; }
    }
}