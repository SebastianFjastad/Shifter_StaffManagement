using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadShiftTemplateResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<ShiftTemplateDto> ShiftTemplates { get; set; }

        public LoadShiftTemplateResponse()
        {
            ShiftTemplates = new List<ShiftTemplateDto>();
        }
    }
}