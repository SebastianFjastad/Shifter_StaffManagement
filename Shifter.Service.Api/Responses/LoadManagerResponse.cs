using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadManagerResponse : GenericServiceResponse
    {
        [DataMember]
        public ManagerDto Manager { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}