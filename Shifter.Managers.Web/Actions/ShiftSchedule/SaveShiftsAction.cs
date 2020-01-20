using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Notifications;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;
using System;

namespace Shifter.Managers.Web.Actions
{
    public class SaveShiftsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public SaveShiftsAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnSuccess { get; set; }
        public Func<NotificationCollection, T> OnError { get; set; }


        #endregion

        #region Methods

        public T Invoke(IEnumerable<EditedShift> shiftEdits)
        {
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");
            Guard.InstanceNotNull(OnError, "OnError");

            var notifications = NotificationCollection.CreateEmpty();
            //if shifts are marked as isSave (checked) then save them (when rendering shifts set the date and time)
            //else if they have an id delete them

            var shiftsToSave = shiftEdits.Where(s => s.IsSave).Select(s => s.Shift).ToList();
            var shiftsToDelete = shiftEdits.Where(s => s.IsDelete).Select(s => s.Shift).ToList();

            if (shiftsToSave.Any())
            {
                foreach (var shift in shiftsToSave)
                {
                    //if end time is smaller than start then it spans 2 days and therefor and extra day is added to the date
                    if (shift.EndTime < shift.StartTime)
                    {
                        shift.EndTime = shift.EndTime.AddDays(1);
                    }
                }

                var result = serviceRegistry.ShiftService.SaveShifts(new ShiftsRequest(shiftsToSave));
                notifications += result.NotificationCollection;
            }

            if (shiftsToDelete.Any())
            {
                notifications += serviceRegistry.ShiftService.DeleteShifts(new ShiftsRequest(shiftsToDelete)).NotificationCollection;
            }

            return notifications.HasErrors() ? OnError(notifications) : OnSuccess();
        }

        #endregion
    }
}