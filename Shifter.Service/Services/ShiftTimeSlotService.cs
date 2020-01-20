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
    public class ShiftTimeSlotService : ShifterServiceBase, IShiftTimeSlotService
    {
        private readonly IShiftTimeSlotDomainService shiftTimeSlotDomainService;

        public ShiftTimeSlotService(IShiftTimeSlotDomainService shiftTimeSlotDomainService)
        {
            Guard.ArgumentNotNull(shiftTimeSlotDomainService, "shiftTimeSlotDomainService");

            this.shiftTimeSlotDomainService = shiftTimeSlotDomainService;
        }

        public LoadShiftTimeslotResponse LoadTimeSlotById(LoadShiftTimeSlotByIdRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var shiftTimeslot = shiftTimeSlotDomainService.LoadTimeSlot(request.TimeSlotId);

                return shiftTimeslot.AsLoadShiftTimeslotResponse();
            });
        }

        public LoadShiftTimeslotCollectionResponse LoadTimeSlots(LoadTimeSlotCollectionRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var shiftTimeslots = shiftTimeSlotDomainService.LoadTimeSlots(request.RestaurantId, request.StaffTypeId);

                return shiftTimeslots.AsLoadShiftTimeslotCollectionResponse();
            });
        }

        public GenericServiceResponse SaveTimeslot(SaveShiftTimeSlotRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = shiftTimeSlotDomainService.SaveTimeslot(
                    MappingUtility.Map<ShiftTimeSlotDto, ShiftTimeslot>(request.ShiftTimeSlot));

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse DeleteTimeslot(DeleteShiftTimeSlotRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = shiftTimeSlotDomainService.DeleteTimeslot(request.ShiftTimeSlotId);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }
    }
}