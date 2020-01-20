using Shifter.Service.Api.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class StaffTypeCollectionResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }

        public StaffTypeCollectionResponse()
        {
            StaffTypes = new List<StaffTypeDto>();
        }
    }
}