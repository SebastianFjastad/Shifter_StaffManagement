using Framework;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class LoadTimeslotsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry shifterSystem;

        #endregion

        #region Constructors

        public LoadTimeslotsAction(IServiceRegistry shifterSystem)
        {
            Guard.ArgumentNotNull(shifterSystem, "shifterSystem");

            this.shifterSystem = shifterSystem;
        }

        #endregion

        #region Properties

        public Func<ShiftTimeslotViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, int staffTypeId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var viewModel = new ShiftTimeslotViewModel();

            var loadTimeSlotsResponse = shifterSystem.ShiftTimeSlotService.LoadTimeSlots(
                new LoadTimeSlotCollectionRequest { RestaurantId = restaurantId, StaffTypeId = staffTypeId });

            viewModel.Timeslots = loadTimeSlotsResponse.ShiftTimeslots;

            return OnComplete.Invoke(viewModel);
        }

        #endregion
    }
}