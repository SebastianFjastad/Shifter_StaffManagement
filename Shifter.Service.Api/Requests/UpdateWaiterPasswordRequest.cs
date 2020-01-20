using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class UpdateWaiterPasswordRequest
    {
        [DataMember]
        public int WaiterId { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}