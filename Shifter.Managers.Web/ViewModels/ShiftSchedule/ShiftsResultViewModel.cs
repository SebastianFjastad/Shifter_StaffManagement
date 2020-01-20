using System.Collections;
using System.Collections.Generic;
using Framework;
using System;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class ShiftsResultViewModel : ShifterModelBase
    {
        public ShiftsResultViewModel(IEnumerable<ShiftDto> shifts)
        {
            Guard.ArgumentNotNull(shifts, "shifts");

            Shifts = shifts;
        }

        #region Properties

        public IEnumerable<ShiftDto> Shifts { get; set; }

        #endregion
        
    }
}