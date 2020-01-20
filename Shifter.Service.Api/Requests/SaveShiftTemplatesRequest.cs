using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveShiftTemplatesRequest
    {
        [DataMember]
        public IEnumerable<ShiftTemplateDto> ShiftTemplates { get; set; }
    }
}