using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class DeleteShiftRequest
    {
        [DataMember]
        public int ShiftId { get; set; }
    }
}