using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadShiftTimeslotResponse : GenericServiceResponse
    {
        [DataMember]
        public ShiftTimeSlotDto ShiftTimeSlot { get; set; }
    }
}