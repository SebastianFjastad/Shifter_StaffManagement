using System;
using System.Collections.Generic;
using System.Linq;
using Framework.CustomTypes;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class ShiftScheduleResultsViewModel : ShifterModelBase
    {
        #region Properties

        public IEnumerable<ShiftTimeSlotDto> Timeslots { get; set; }

        public IEnumerable<ShiftDto> Shifts { get; set; }

        public IEnumerable<StaffDto> Waiters { get; set; }

        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }

        public DateTime? WeekStartDate { get; set; }

        #endregion

        #region Methods

        public StaffTypeDto GetFirstStaffType()
        {
            return StaffTypes.FirstOrDefault(staffType => Waiters.Any(w => w.StaffType.Id == staffType.Id));
        }

        public IEnumerable<ShiftDto> GetShifts(DayOfWeekStartingAtMonday day)
        {
            return Shifts.Where(s => s.StartTime.DayOfWeek.ToString() == day.ToString()).OrderBy(s => s.StartTime).ToList();
        }

        public IEnumerable<ShiftDto> FindShifts(DayOfWeekStartingAtMonday day, int waiterId)
        {
            return Shifts.Where(s => s.StartTime.DayOfWeek.ToString() == day.ToString() && s.Staff != null && s.Staff.Id == waiterId);
        }
        public IEnumerable<ScheduleTimeslotsViewModel> GetTimeslotSummariesForDay(DayOfWeekStartingAtMonday day)
        {
            var shifts = GetShifts(day);

            var timeslotSummary = new List<ScheduleTimeslotsViewModel>();

            foreach (var timeSlot in Timeslots)
            {
                var shiftsForTimeslot = shifts.Where(s => s.StartTime.TimeOfDay == timeSlot.StartTime && s.EndTime.TimeOfDay == timeSlot.EndTime).ToList();
                timeslotSummary.Add(new ScheduleTimeslotsViewModel()
                {
                    Timeslot = timeSlot,
                    TotalShifts = shiftsForTimeslot.Count(),
                });
            }
            return timeslotSummary;
        }

        #endregion
    }

    public class ScheduleTimeslotsViewModel
    {
        public ShiftTimeSlotDto Timeslot { get; set; }

        public int TotalShifts { get; set; }
    }
}