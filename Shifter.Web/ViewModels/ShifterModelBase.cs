using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Notifications;
using Shifter.Service.Api.Dtos;

namespace Shifter.Web.ViewModels
{
    public abstract class ShifterModelBase
    {
        #region Constructor

        /// <summary>
        /// Base view model to bundle common functionality
        /// </summary>
        protected ShifterModelBase()
        {
            this.Notifications = NotificationCollection.CreateEmpty();
        }

        #endregion

        #region Properties

        public NotificationCollection Notifications { get; set; }

        public NotificationCollection Errors
        {
            get
            {
                return NotificationCollection.Create(Notifications.Where(n => n.Severity == NotificationSeverity.Error).ToList());
            }
        }

        public bool HasErrors
        {
            get
            {
                return this.Errors.Any();
            }
        }

        #endregion

        #region Methods

        public bool CanWaiterWorkThisShift(IEnumerable<ShiftDto> waitersShifts, ShiftDto shift )
        {
            var alreadyWorking = waitersShifts.Any(s => (shift.StartTime >= s.StartTime && shift.StartTime < s.EndTime) || (s.StartTime >= shift.StartTime && s.StartTime < shift.EndTime));

            return shift.IsAvailable && !alreadyWorking && shift.StartTime > DateTime.Now;
        }

        #endregion
    }
}