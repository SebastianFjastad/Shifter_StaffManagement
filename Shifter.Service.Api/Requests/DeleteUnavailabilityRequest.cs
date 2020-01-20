using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class DeleteUnavailabilityRequest
    {
        [DataMember]
        public IEnumerable<int> UnavailabilityIds { get; set; }
    }
}