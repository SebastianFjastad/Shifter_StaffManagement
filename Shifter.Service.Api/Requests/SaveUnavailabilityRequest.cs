using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveUnavailabilityRequest
    {
        [DataMember]
        public IEnumerable<StaffUnavailabilityRecordDto> Unavailability { get; set; }
        
        [DataMember]
        public int StaffId { get; set; }
    }
}