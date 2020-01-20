using Framework;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class DeleteShiftAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public DeleteShiftAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<ShiftsResultViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int shiftId, int restaurantId, int waiterId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var existingShiftResult = serviceRegistry.ShiftService.LoadShift(new GenericEntityRequest(shiftId));

            var deleteShiftResponse = serviceRegistry.ShiftService.DeleteShift(new DeleteShiftRequest { ShiftId = shiftId });

            var shiftsResponse = serviceRegistry.ShiftService.LoadShifts(new LoadShiftsRequest() { FromDate = existingShiftResult.Shift.StartTime, ToDate = existingShiftResult.Shift.EndTime, RestaurantId = restaurantId, StaffId = waiterId });

            return OnComplete(new ShiftsResultViewModel(shiftsResponse.Shifts) { Notifications = deleteShiftResponse.NotificationCollection });
        }

        #endregion
    }
}