using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class StaffHoursResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<StaffHours> StaffHoursCollection { get; set; }

        public StaffHoursResponse()
        {
            StaffHoursCollection = new List<StaffHours>();
        }
    }
}