using System.Collections.Generic;
using System.Linq;
using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class LoadShiftTemplateResponseExtensions
    {
        public static LoadShiftTemplateResponse AsLoadShiftTemplateResponse(this IEnumerable<ShiftTemplate> shiftTemplates)
        {
            Guard.InstanceNotNull(shiftTemplates, "shiftTemplates");

            var result = new LoadShiftTemplateResponse();

            result.ShiftTemplates = shiftTemplates.Select(MappingUtility.Map<ShiftTemplate, ShiftTemplateDto>);

            return result;
        }
    }
}