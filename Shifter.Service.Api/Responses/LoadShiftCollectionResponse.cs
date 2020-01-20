using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadShiftCollectionResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<ShiftDto> Shifts { get; set; } 
    }
}