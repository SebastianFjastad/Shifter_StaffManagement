using System;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class UpdateLastActiveDateRequest
    {
        [DataMember]
        public int StaffMemberId { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}