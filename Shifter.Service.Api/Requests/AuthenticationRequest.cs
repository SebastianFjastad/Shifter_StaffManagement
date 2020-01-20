using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class AuthenticationRequest
    {
        [DataMember(IsRequired = true)]
        public string Username { get; set; }
        
        [DataMember(IsRequired = true)]
        public string Password { get; set; }
    }
}