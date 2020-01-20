using System.Runtime.Serialization;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class PasswordResetResponse : GenericServiceResponse
    {
        [DataMember]
        public string EmailAddress { get; set; }
    }
}