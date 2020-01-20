using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadShiftTimeSlotByIdRequest
    {
        [DataMember]
        public int TimeSlotId { get; set; }
    }
}