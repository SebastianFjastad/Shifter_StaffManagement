using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveSettingsRequest
    {
        [DataMember(IsRequired = true)]
        public SettingsDto Settings { get; set; }
    }
}