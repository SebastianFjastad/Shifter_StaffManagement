using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class AssignShiftRequest
    {
        [DataMember]
        public int StaffId { get; set; }

        [DataMember]
        public int ShiftId { get; set; }

        [DataMember]
        public string ClientKey { get; set; }
    }
}