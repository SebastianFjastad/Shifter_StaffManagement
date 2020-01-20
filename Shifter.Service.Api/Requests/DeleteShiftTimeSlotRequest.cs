using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class DeleteShiftTimeSlotRequest
    {
        [DataMember]
        public int ShiftTimeSlotId { get; set; }
    }
}