using Framework.Extensions;
using Shifter.Waiters.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Waiters.Web.ViewModels
{
    public class ShiftScheduleResultsViewModel : ShifterModelBase
    {
        #region Properties

        public IEnumerable<Shift> WaitersShifts
        {
            get
            {
                return AllShifts.Where(s => s.Waiter != null && s.Waiter.Id == Waiter.Id);
            }
        }

        /// <summary>
        /// All the shifts for the selected week
        /// </summary>
        public IEnumerable<Shift> AllShifts { get; set; }

        public Waiter Waiter { get; set; }

        /// <summary>
        /// Waiters that have shifts for the currently selected week
        /// </summary>
        public IEnumerable<Waiter> Waiters { get; set; }

        #endregion

        #region Methods

        public IEnumerable<Shift> GetShifts(DayOfWeek day)
        {
            return AllShifts.Where(s => s.Date.DayOfWeek == day).OrderBy(s => s.StartTime).ToList();
        }

        public IEnumerable<Shift> FindShifts(DayOfWeek day, int waiterId)
        {
            return AllShifts.Where(s => s.Date.DayOfWeek == day && s.Waiter != null && s.Waiter.Id == waiterId);
        }

        public bool BelongsToWaiter(Shift shift)
        {
            return shift.Waiter.IsNotNull() && shift.Waiter.Id == Waiter.Id;
        }

        public IEnumerable<Shift> GetAvailableShifts(DayOfWeek day)
        {
            return AllShifts.Where(s => s.Date.DayOfWeek == day && s.IsAvailable).ToList();
        }

        public bool GetAvailablityUpdateValue(Shift shift)
        {
            return shift.IsAvailable ? false : true;
        }

        public bool WaiterAlreadyWorking(Shift shift)
        {
            return Waiter.Shifts.Any(s => s.Date == shift.Date && s.StartTime == shift.StartTime);
        }

        #endregion
    }
}