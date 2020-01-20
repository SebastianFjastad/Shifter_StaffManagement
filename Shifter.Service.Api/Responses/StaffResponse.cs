using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class StaffResponse : GenericServiceResponse
    {
        [DataMember]
        public StaffDto Staff { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}