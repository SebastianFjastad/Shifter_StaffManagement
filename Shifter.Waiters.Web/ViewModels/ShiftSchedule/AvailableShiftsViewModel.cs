using System;
using System.Collections.Generic;
using System.Linq;
using Framework.CustomTypes;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.Helpers;
using Shifter.Waiters.Web.Utils;

namespace Shifter.Waiters.Web.ViewModels
{
    public class AvailableShiftsViewModel : ViewModelBase
    {
        #region Constructors

        public AvailableShiftsViewModel()
        {
            WaitersShifts = new List<ShiftDto>();
            AvailableShifts = new List<ShiftDto>();
        }

        #endregion

        #region Properties

        public IEnumerable<ShiftDto> WaitersShifts { get; set; }

        /// <summary>
        /// All the available shifts for the selected week
        /// </summary>
        public IEnumerable<ShiftDto> AvailableShifts { get; set; }

        public StaffDto Staff { get; set; }

        public int WeekNumber { get; set; }

        #endregion

        #region Methods

        public IEnumerable<ShiftDto> GetWaitersShifts(DayOfWeekStartingAtMonday day)
        {
            return WaitersShifts.Where(s => s.StartTime.DayOfWeek.ToString() == day.ToString()).OrderBy(s => s.StartTime).ToList();
        }

        public IEnumerable<ShiftDto> GetAvailableShifts(DayOfWeekStartingAtMonday day)
        {
            return AvailableShifts.Where(s => s.StartTime.DayOfWeek.ToString() == day.ToString()).ToList();
        }

        public bool CanWaiterWorkAvailableTimes(IEnumerable<ShiftDto> shifts)
        {
            //Is the waiter shifted on all the available times
            var waiterHasAvailability = false;
            foreach (var shift in shifts)
            {
                if (CanWaiterWorkShift(shift))
                {
                    waiterHasAvailability = true;
                }
            }
            return waiterHasAvailability;
        }

        #endregion

        public bool CanWaiterWorkShift(ShiftDto shift)
        {
            return CanWaiterWorkThisShift(WaitersShifts, shift) && !Staff.IsUnavailableOnDate(shift.StartTime);
        }

        public bool HasDayPassed(DayOfWeekStartingAtMonday day)
        {
            return Dates.GetDateFromDayAndWeek((int)day, WeekNumber) < DateTime.Today;
        }
    }
}