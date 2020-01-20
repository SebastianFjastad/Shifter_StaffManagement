using System;
using System.Collections.Generic;
using System.Linq;
using Shifter.Service.Api.Dtos;

namespace Shifter.Waiters.Web.ViewModels.ShiftSchedule
{
    public class ShiftsToTakeViewModel
    {
        #region Properties

        public IEnumerable<ShiftDto> Shifts { get; set; }

        public IEnumerable<ShiftDto> DistinctShiftsByStartTime
        {
            get
            {
                return Shifts.GroupBy(s => s.StartTime).Select(g => g.First()).ToList();
            }
        }

        public DateTime DateOfShiftsToTake { get; set; }

        #endregion
    }
}