using System;
using System.Collections.Generic;
using System.Linq;
using Shifter.Service.Api.Dtos;

namespace Shifter.Waiters.Web.ViewModels
{
    public class AllShiftsViewModel : ViewModelBase
    {
        #region Constructors

        public AllShiftsViewModel()
        {
            Waiters = new List<StaffDto>();
        }

        #endregion

        #region Properties

        public StaffDto Staff { get; set; }

        public IEnumerable<StaffDto> Waiters { get; set; }

        public IEnumerable<StaffDto> OrderedWaiters
        {
            get
            {
                var waitersWithoutShifts = Waiters.Where(w => w.Id != Staff.Id && !w.Shifts.Any());
                var waitersWithShifts = Waiters.Where(w => w.Id != Staff.Id && w.Shifts.Any());
                var waiters = new List<StaffDto>();

                waiters.AddRange(waitersWithShifts.OrderBy(w => w.Shifts.First().StartTime).ThenBy(w => w.FirstName));
                waiters.AddRange(waitersWithoutShifts.OrderBy(w => w.FirstName));

                return waiters.ToList();
            }
        }

        public DateTime SelectedDay { get; set; }

        #endregion

        #region Methods

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
            return CanWaiterWorkThisShift(Staff.Shifts, shift);
        }
    }
}