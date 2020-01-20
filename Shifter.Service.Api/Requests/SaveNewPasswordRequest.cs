using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveNewPasswordRequest
    {
        [DataMember]
        public int UserAccountId { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}