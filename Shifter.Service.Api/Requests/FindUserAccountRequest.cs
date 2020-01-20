using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class FindUserAccountRequest
    {
        [DataMember(IsRequired = true)]
        public string Username { get; set; }
    }
}