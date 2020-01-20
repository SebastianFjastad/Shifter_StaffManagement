using Framework;
using Framework.Notifications.Extensions;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class SaveTimeslotAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry shifterSystem;

        #endregion

        #region Constructors

        public SaveTimeslotAction(IServiceRegistry shifterSystem)
        {
            Guard.ArgumentNotNull(shifterSystem, "shifterSystem");

            this.shifterSystem = shifterSystem;
        }

        #endregion

        #region Properties

        public Func<ShiftTimeslotViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(string startTime, string endTime, int restaurantId, int staffTypeId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");
            Guard.InstanceNotNull(startTime, "startTime");
            Guard.InstanceNotNull(endTime, "endTime");

            var viewModel = new ShiftTimeslotViewModel();

            if (startTime.Trim().IsNotNullOrEmpty() && endTime.Trim().IsNotNullOrEmpty())
            {
                var timeslot = new ShiftTimeSlotDto
                {
                    StartTime = TimeSpan.Parse(startTime), 
                    EndTime = TimeSpan.Parse(endTime), 
                    RestaurantId = restaurantId,
                    StaffTypeId = staffTypeId
                };

                var saveTimeSlotResponse = shifterSystem.ShiftTimeSlotService.SaveTimeslot(new SaveShiftTimeSlotRequest{ShiftTimeSlot = timeslot});

                viewModel.Notifications += saveTimeSlotResponse.NotificationCollection;
            }
            else
            {
                viewModel.Notifications.AddError("Times cannot be empty");
            }

            var loadTimeSlotsResponse = shifterSystem.ShiftTimeSlotService.LoadTimeSlots(new LoadTimeSlotCollectionRequest {RestaurantId = restaurantId, StaffTypeId = staffTypeId});

            viewModel.Timeslots = loadTimeSlotsResponse.ShiftTimeslots;

            return OnComplete.Invoke(viewModel);
        }

        #endregion

    }
}