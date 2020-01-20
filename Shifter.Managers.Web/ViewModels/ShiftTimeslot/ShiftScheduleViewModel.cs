using System.Collections.Generic;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class ShiftTimeslotViewModel : ShifterModelBase
    {
        #region Properties

        public IEnumerable<ShiftTimeSlotDto> Timeslots { get; set; }

        #endregion

        #region Methods


        #endregion
    }
}