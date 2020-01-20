using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class StaffCollectionResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<StaffDto> Staff { get; set; }
        
        [DataMember]
        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }

        public StaffCollectionResponse()
        {
            Staff = new List<StaffDto>();
            StaffTypes = new List<StaffTypeDto>();
        }
    }
}