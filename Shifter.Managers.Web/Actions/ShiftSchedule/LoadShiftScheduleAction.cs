using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class LoadShiftScheduleAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadShiftScheduleAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<ShiftScheduleResultsViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, DateTime? weekFrom, DateTime? weekTo, IEnumerable<int> staffTypeIds)
        {
            var viewModel = new ShiftScheduleResultsViewModel();

            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var shiftsRequest = new LoadShiftsRequest
            {
                RestaurantId = restaurantId,
                FromDate = weekFrom,
                ToDate = weekTo,
                StaffTypeIds = staffTypeIds
            };

            var staffRequest = new LoadStaffListRequest()
            {
                RestaurantId = restaurantId,
                StaffTypeIds = staffTypeIds
            };

            Parallel.Invoke(() =>
            {
                var loadShiftResponse = serviceRegistry.ShiftService.LoadShifts(shiftsRequest);
                viewModel.Shifts = loadShiftResponse.Shifts;
            },
            () =>
            {
                var loadWaiterResponse = serviceRegistry.StaffService.LoadStaffList(staffRequest);
                viewModel.Waiters = loadWaiterResponse.Staff;
            },
            () =>
            {
                var loadShiftTimeSlotsRepsonse = serviceRegistry.ShiftTimeSlotService.LoadTimeSlots(new LoadTimeSlotCollectionRequest { RestaurantId = restaurantId });
                viewModel.Timeslots = loadShiftTimeSlotsRepsonse.ShiftTimeslots;
            },
            () =>
            {
                var staffTypesResponse = serviceRegistry.StaffService.LoadStaffTypes();
                viewModel.StaffTypes = staffTypesResponse.StaffTypes;
            });

            viewModel.WeekStartDate = weekFrom;

            return OnComplete.Invoke(viewModel);
        }

        #endregion
    }
}