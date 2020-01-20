using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class UpdateManagerPasswordRequest
    {
        [DataMember]
        public int ManagerId { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}