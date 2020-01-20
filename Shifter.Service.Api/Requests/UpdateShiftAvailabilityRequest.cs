using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class UpdateShiftAvailabilityRequest
    {
        [DataMember]
        public int ShiftId { get; set; }

        [DataMember]
        public bool MakeAvailable { get; set; }
        
        [DataMember]
        public string ClientKey { get; set; }
    }
}