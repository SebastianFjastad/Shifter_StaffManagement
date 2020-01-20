using System.Linq;
using Framework;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class LoadWaitersShiftsForDateAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadWaitersShiftsForDateAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<ShiftEditorViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, int staffId, DateTime date)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var viewModel = new ShiftEditorViewModel();

            viewModel.EditForDate = date;
            viewModel.WaiterId = staffId;
            viewModel.RestaurantId = restaurantId;

            var loadStaffResult = serviceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest(){StaffId = staffId});

            Guard.InstanceNotNull(loadStaffResult.Staff, "loadStaffResult.Staff");

            var shiftsResponse = serviceRegistry.ShiftService.LoadShifts(new LoadShiftsRequest() {FromDate = date, ToDate = date, RestaurantId = restaurantId, StaffId = staffId});
            viewModel.Shifts = shiftsResponse.Shifts;

            var loadTimeSlotsResponse = serviceRegistry.ShiftTimeSlotService.LoadTimeSlots(new LoadTimeSlotCollectionRequest { RestaurantId = restaurantId, StaffTypeId = loadStaffResult.Staff.StaffType.Id});
            viewModel.TimeSlots = loadTimeSlotsResponse.ShiftTimeslots;

            return OnComplete.Invoke(viewModel);
        }

        #endregion
    }
}