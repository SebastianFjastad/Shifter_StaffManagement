using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class FindManagerRequest
    {
        [DataMember]
        public string Username { get; set; }
    }
}