using Framework;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class DeleteTimeslotAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry shifterSystem;

        #endregion

        #region Constructors

        public DeleteTimeslotAction(IServiceRegistry shifterSystem)
        {
            Guard.ArgumentNotNull(shifterSystem, "shifterSystem");

            this.shifterSystem = shifterSystem;
        }

        #endregion

        #region Properties

        public Func<ShiftTimeslotViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(ShiftTimeSlotDto timeslot)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");
            Guard.InstanceNotNull(timeslot, "timeslot");

            var viewModel = new ShiftTimeslotViewModel();

            var deleteTimeSlotResponse = shifterSystem.ShiftTimeSlotService.DeleteTimeslot(new DeleteShiftTimeSlotRequest{ShiftTimeSlotId = timeslot.Id});

            viewModel.Notifications = deleteTimeSlotResponse.NotificationCollection;

            var loadTimeSlotsService = shifterSystem.ShiftTimeSlotService.LoadTimeSlots(new LoadTimeSlotCollectionRequest {RestaurantId = timeslot.RestaurantId, StaffTypeId = timeslot.StaffTypeId});

            viewModel.Timeslots = loadTimeSlotsService.ShiftTimeslots;

            return OnComplete.Invoke(viewModel);
        }

        #endregion

    }
}