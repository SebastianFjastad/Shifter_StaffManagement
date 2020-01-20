using Framework;
using System;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class ShiftSummaryViewModel : ShifterModelBase
    {
        public ShiftSummaryViewModel(ShiftDto shift)
        {
            Guard.ArgumentNotNull(shift, "shift");

            Shift = shift;
        }

        #region Properties

        public ShiftDto Shift { get; set; }

        #endregion

        #region Methods
        public bool ShiftIsInThePast()
        {
            return Shift.StartTime < DateTime.Now;
        }

        public bool ShowAvailabilityHint()
        {
            return Shift.IsAvailable && Shift.Staff.IsNotNull() && !ShiftIsInThePast();
        }

        #endregion
    }
}