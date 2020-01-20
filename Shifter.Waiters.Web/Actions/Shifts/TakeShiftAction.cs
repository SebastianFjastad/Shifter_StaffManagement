using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Service.Api.Requests;

namespace Shifter.Waiters.Web.Actions.Shifts
{
    public class TakeShiftAction<T> where T : class 
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public TakeShiftAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnSuccess { get; set; } 

        public Func<NotificationCollection, T> OnFail { get; set; } 

        #endregion

        #region Methods

        public T Invoke(int shiftId, int waiterId, int restaurantId, string clientKey)
        {
            var result = NotificationCollection.CreateEmpty();

            result += Validate(waiterId, shiftId, restaurantId);

            if (!result.HasErrors())
            {
                var assignShiftResponse = serviceRegistry.ShiftService.AssignShift(new AssignShiftRequest {StaffId = waiterId, ShiftId = shiftId, ClientKey = clientKey});

                result += assignShiftResponse.NotificationCollection;
            }

            return result.HasErrors() ? OnFail.Invoke(result.Errors()) : OnSuccess.Invoke();
        }

        //TODO This needs to be moved to the service or domain layer... Return error codes with messages and only staff are interested in staff specific and general errors and not manager errors. eg. only return errors with codes starting with STA and GEN but not MAN
        private NotificationCollection Validate(int waiterId, int shiftId, int restaurantId)
        {
            var result = NotificationCollection.CreateEmpty();

            var settingsResult = serviceRegistry.SettingsService.LoadSettingsByRestaurant(new LoadSettingsByRestaurantRequest() { RestaurantId = restaurantId });
            var shiftResult = serviceRegistry.ShiftService.LoadShift(new GenericEntityRequest(shiftId));

            var shiftDate = shiftResult.Shift.StartTime.Date;
            var loadWaiterRequest = new LoadStaffMemberRequest { StaffId = waiterId, IncludeShiftsFrom = shiftDate.StartOfWeek(), IncludeShiftsTo = shiftDate.StartOfWeek().AddDays(6), RestaurantId = restaurantId };

            var waiterResult = serviceRegistry.StaffService.LoadStaffMember(loadWaiterRequest);

            if (waiterResult.Staff.MaxNumberOfShiftsPerWeek <= waiterResult.Staff.Shifts.Count())
            {
                result.AddError("You are already working your max number of shifts for this week. Speak to your manager if you would like to work more.");
            }

            if (!waiterResult.Staff.CanSwapShifts)
            {
                result.AddError("You are not allowed to swap shifts, your manager can enable this on your profile.");
            }

            //If waiter is trying to take from another waiter and the shift has reached its "sell-by date"
            if (shiftResult.Shift.Staff.IsNotNull() && settingsResult.Settings.NumDaysBeforeShiftSwappingLockDown >= shiftResult.Shift.NumDaysTillDueDate)
            {
                result.AddError(string.Format("Shifts can't be swapped when the due date is less than {0} days away.", settingsResult.Settings.NumDaysBeforeShiftSwappingLockDown + 1));
            }

            //Cant work doubles and is trying to take shift on a date he is already working but at a different time
            if (!waiterResult.Staff.CanWorkDoubles && waiterResult.Staff.Shifts.Any(s => s.StartTime.Date == shiftResult.Shift.StartTime.Date && s.StartTime != shiftResult.Shift.StartTime))
            {
                result.AddError("You are not allowed to work doubles, your manager can enable this on your profile.");
            }

            return result;
        }

        #endregion

    }
}