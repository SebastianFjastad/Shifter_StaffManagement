using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class LoadSettingsResponseExtensions
    {
        public static LoadSettingsResponse AsLoadSettingsResponse(this Settings settings)
        {
            Guard.InstanceNotNull(settings, "settings");

            var result = new LoadSettingsResponse();

            result.Settings = MappingUtility.Map<Settings, SettingsDto>(settings);

            return result;
        }
    }
}