using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveShiftTimeSlotRequest
    {
        [DataMember]
        public ShiftTimeSlotDto ShiftTimeSlot { get; set; }
    }
}