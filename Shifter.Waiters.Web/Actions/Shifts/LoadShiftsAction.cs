using System.Collections.Generic;
using Shifter.Service.Api.Requests;
using Shifter.Waiters.Web.ViewModels.ShiftSchedule;
using System;
using System.Linq;

namespace Shifter.Waiters.Web.Actions.Shifts
{
    public class LoadShiftsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadShiftsAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<ShiftsToTakeViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, DateTime shiftDate, int loggedInWaiterId, int? selectedWaiterId)
        {
            //TODO unnecessarily large call
            var loadWaiterResponse = serviceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest { StaffId = loggedInWaiterId });

            var ids = new List<int>() {loadWaiterResponse.Staff.StaffType.Id};

            var shiftsResponse = serviceRegistry.ShiftService.LoadShifts(new LoadShiftsRequest { RestaurantId = restaurantId, FromDate = shiftDate, ToDate = shiftDate, IsAvailable = true, StaffTypeIds = ids });
            
            var availableShifts = shiftsResponse.Shifts.Where(s => s.IsAvailable);
            var waiterShifts = shiftsResponse.Shifts.Where(s => s.Staff.Id == loggedInWaiterId);

            //filter out shifts that this waiter is already working
            if (waiterShifts.Any())
            {
                availableShifts = availableShifts.Where(s => !waiterShifts.Any(ws => ws.StartTime == s.StartTime)).ToList();
            }

            //When clicking on a specific waiters available shifts you only want to see their shifts and not all available shifts
            if (selectedWaiterId.IsNotNull())
            {
                availableShifts = availableShifts.Where(s => s.Staff.IsNotNull() && s.Staff.Id == selectedWaiterId).ToList();
            }

            var model = new ShiftsToTakeViewModel();

            model.Shifts = availableShifts.OrderBy(s =>s.StartTime).ThenBy(s =>s.Staff.IsNull());
            model.DateOfShiftsToTake = shiftDate;

            return OnComplete.Invoke(model);
        }

        #endregion

    }
}