using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadShiftTimeslotCollectionResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<ShiftTimeSlotDto> ShiftTimeslots { get; set; }
    }
}