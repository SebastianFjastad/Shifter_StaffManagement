using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadWaiterByUsernameRequest
    {
        [DataMember]
        public string Username { get; set; }
    }
}