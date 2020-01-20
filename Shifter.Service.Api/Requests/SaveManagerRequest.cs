using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveManagerRequest
    {
        [DataMember]
        public ManagerDto Manager { get; set; }
    }
}