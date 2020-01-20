using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class ShiftsRequest
    {
        public ShiftsRequest(IEnumerable<ShiftDto> shifts)
        {
            Shifts = shifts;
        }

        [DataMember]
        public IEnumerable<ShiftDto> Shifts { get; set; }
    }
}