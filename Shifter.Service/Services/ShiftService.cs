using System;
using System.Collections.Generic;
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
    public class ShiftService : ShifterServiceBase, IShiftService
    {
        private readonly IShiftDomainService shiftDomainService;

        public ShiftService(IShiftDomainService shiftDomainService)
        {
            Guard.ArgumentNotNull(shiftDomainService, "shiftDomainService");

            this.shiftDomainService = shiftDomainService;
        }

        public LoadShiftCollectionResponse LoadShifts(LoadShiftsRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var shifts = shiftDomainService.LoadShifts(request.RestaurantId, request.TimeSlotId, request.FromDate, request.ToDate, request.IsAssigned, request.IsAvailable, request.StaffTypeIds, request.StaffId);

                return shifts.AsLoadShiftCollectionResponse();
            });
        }

        public LoadShiftResponse LoadShift(GenericEntityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var shift = shiftDomainService.LoadShift(request.EntityId);

                return shift.AsLoadShiftResponse();
            });
        }

        public GenericServiceResponse SaveShift(SaveShiftRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = shiftDomainService.SaveShift(request.Shift.AsDomainModel());

                return new GenericServiceResponse { NotificationCollection = notificationCollection };
            });
        }

        public GenericServiceResponse DeleteShift(DeleteShiftRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = shiftDomainService.DeleteShift(request.ShiftId);

                return new GenericServiceResponse { NotificationCollection = notificationCollection };
            });
        }

        public GenericServiceResponse AssignShift(AssignShiftRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = shiftDomainService.AssignShift(
                    request.StaffId, request.ShiftId, request.ClientKey);

                return new GenericServiceResponse { NotificationCollection = notificationCollection };
            });
        }

        public GenericServiceResponse UpdateShiftAvailablity(UpdateShiftAvailabilityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = shiftDomainService.UpdateShiftAvailablity(
                    request.ShiftId, request.MakeAvailable, request.ClientKey);

                return new GenericServiceResponse { NotificationCollection = notificationCollection };
            });
        }

        public CopyShiftsResponse CopyShifts(CopyShiftsRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new CopyShiftsResponse();

                //Remove
                var numDaysToCopy = (request.CopyFromEndDate - request.CopyFromStartDate).Days;

                var copyToEndDate = request.CopyToStartDate.AddDays(numDaysToCopy);

                var existingShifts = shiftDomainService.LoadShifts(request.RestaurantId, null, request.CopyToStartDate, copyToEndDate, null, null, request.StaffTypeIds, null);

                if (existingShifts.Any())
                {
                    if (request.OverwriteExistingShifts == false)
                    {
                        result.HasConflictWarning = true;
                    }
                    else
                    {
                        result.NotificationCollection += shiftDomainService.DeleteShifts(existingShifts);
                    }
                }

                //Copy
                var shiftsToCopy = shiftDomainService.LoadShifts(request.RestaurantId, null, request.CopyFromStartDate, request.CopyFromEndDate, null, null, request.StaffTypeIds, null);

                var daysToAdd = (request.CopyToStartDate - request.CopyFromStartDate).Days;

                foreach (var shift in shiftsToCopy)
                {
                    shift.Id = 0;
                    shift.StartTime = shift.StartTime.AddDays(daysToAdd);
                    shift.EndTime = shift.EndTime.AddDays(daysToAdd);
                }

                result.NotificationCollection += shiftDomainService.SaveShifts(shiftsToCopy);

                return result;
            });
        }

        public GenericServiceResponse SaveShifts(ShiftsRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new GenericServiceResponse();

                var shifts = request.Shifts.Select(s => s.AsDomainModel()).ToList();

                result.NotificationCollection += shiftDomainService.SaveShifts(shifts);

                return result;
            });
        }

        public GenericServiceResponse DeleteShifts(ShiftsRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new GenericServiceResponse();

                var shifts = request.Shifts.Select(s => s.AsDomainModel()).ToList();

                result.NotificationCollection += shiftDomainService.DeleteShifts(shifts);

                return result;
            });
        }
    }
}