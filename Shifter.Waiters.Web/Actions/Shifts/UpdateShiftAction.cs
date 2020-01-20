using System;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Service.Api.Requests;

namespace Shifter.Waiters.Web.Actions.Shifts
{
    public class UpdateShiftAction<T> where T : class 
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public UpdateShiftAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnSuccess { get; set; }

        public Func<NotificationCollection, T> OnFail { get; set; } 

        #endregion

        #region Methods

        public T Invoke(int shiftId, int waiterId, int restaurantId, bool makeAvailable, string clientKey)
        {
            var request = new UpdateShiftAvailabilityRequest
            {
                ShiftId = shiftId,
                MakeAvailable = makeAvailable,
                ClientKey = clientKey
            };

            var result = Validate(waiterId, restaurantId, shiftId);

            if (!result.HasErrors())
            {
                result += serviceRegistry.ShiftService.UpdateShiftAvailablity(request).NotificationCollection;
            }

            return result.HasErrors() ? OnFail.Invoke(result) : OnSuccess.Invoke();
        }

        //TODO This needs to be moved to the service or domain layer... 
        private NotificationCollection Validate(int waiterId, int restaurantId, int shiftId)
        {
            var result = NotificationCollection.CreateEmpty();

            //TODO Create a generic LoadEntityById<T> 
            var waiterResult = serviceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest { StaffId = waiterId });
            var settingsResult = serviceRegistry.SettingsService.LoadSettingsByRestaurant(new LoadSettingsByRestaurantRequest() { RestaurantId = restaurantId });
            var shiftResult = serviceRegistry.ShiftService.LoadShift(new GenericEntityRequest(shiftId));

            if (!waiterResult.Staff.CanSwapShifts)
            {
                result.AddError("Sorry you are not allowed to swap shifts, you need to ask your manager to enable this on your profile.");
            }

            if (settingsResult.Settings.IsNotNull() && shiftResult.Shift.StartTime > DateTime.Now && settingsResult.Settings.NumDaysBeforeShiftSwappingLockDown >= shiftResult.Shift.NumDaysTillDueDate)
            {
                result.AddError(string.Format("Sorry shifts can't be swapped when the due date is less than {0} days away.", settingsResult.Settings.NumDaysBeforeShiftSwappingLockDown + 1));
            }

            if (shiftResult.Shift.StartTime <= DateTime.Now)
            {
                result.AddError("Sorry you can't update shifts in the past.");
            }

            return result;
        }

        #endregion

    }
}