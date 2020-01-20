using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadSettingsResponse : GenericServiceResponse
    {
        [DataMember]
        public SettingsDto Settings { get; set; }
    }
}