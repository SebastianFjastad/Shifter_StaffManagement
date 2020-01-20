using System.Linq;
using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;
using Shifter.Service.Utilities;

namespace Shifter.Service.Services
{
    public class ShiftTemplateService : ShifterServiceBase, IShiftTemplateService
    {
        private readonly IShiftTemplateDomainService shiftTemplateDomainService;

        public ShiftTemplateService(IShiftTemplateDomainService shiftTemplateDomainService)
        {
            Guard.ArgumentNotNull(shiftTemplateDomainService, "shiftTemplateDomainService");

            this.shiftTemplateDomainService = shiftTemplateDomainService;
        }

        public LoadShiftTemplateResponse LoadTemplates(LoadShiftTemplateByRestaurantIdRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var shiftTemplates = shiftTemplateDomainService.LoadTemplates(request.RestaurantId);

                return shiftTemplates.AsLoadShiftTemplateResponse();
            });
        }

        public GenericServiceResponse SaveTemplates(SaveShiftTemplatesRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = shiftTemplateDomainService.SaveTemplates(
                    request.ShiftTemplates.Select(MappingUtility.Map<ShiftTemplateDto, ShiftTemplate>).ToList());

                var result = new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };

                return result;
            });
        }
    }
}