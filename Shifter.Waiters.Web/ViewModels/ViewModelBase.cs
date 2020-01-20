using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Notifications;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Waiters.Web.ViewModels
{
    public abstract class ViewModelBase : ShifterModelBase
    {
        #region Methods

        public bool CanWaiterWorkThisShift(IEnumerable<ShiftDto> waitersShifts, ShiftDto shift )
        {
            var alreadyWorking = waitersShifts.Any(s => (shift.StartTime >= s.StartTime && shift.StartTime < s.EndTime) || (s.StartTime >= shift.StartTime && s.StartTime < shift.EndTime));

            return shift.IsAvailable && !alreadyWorking && shift.StartTime > DateTime.Now;
        }

        #endregion
    }
}