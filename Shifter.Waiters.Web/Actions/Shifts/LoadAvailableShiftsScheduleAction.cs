using Framework;
using Shifter.Service.Api.Requests;
using Shifter.Waiters.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Waiters.Web.Actions.Shifts
{
    public class LoadAvailableShiftsScheduleAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadAvailableShiftsScheduleAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<AvailableShiftsViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int waiterId, int restaurantId, DateTime? weekFrom, DateTime? weekTo, int weekNumber)
        {
            var model = new AvailableShiftsViewModel();

            var loadWaiterResponse = serviceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest { StaffId = waiterId });

            var loadShiftRequest = new LoadShiftsRequest
            {
                RestaurantId = restaurantId,
                FromDate = weekFrom,
                ToDate = weekTo,
                StaffTypeIds = new List<int> { loadWaiterResponse.Staff.StaffType.Id }
            };

            var loadShiftsResponse = serviceRegistry.ShiftService.LoadShifts(loadShiftRequest);

            Guard.InstanceNotNull(loadShiftsResponse.Shifts, "loadShiftsResponse.Shifts");

            model.AvailableShifts = loadShiftsResponse.Shifts.Where(s => s.IsAvailable && (s.Staff.IsNull() || s.Staff.Id != waiterId));
            model.WaitersShifts = loadShiftsResponse.Shifts.Where(s => s.Staff.IsNotNull() && s.Staff.Id == waiterId);

            model.Staff = loadWaiterResponse.Staff;

            model.WeekNumber = weekNumber;

            return OnComplete.Invoke(model);
        }

        #endregion

    }
}