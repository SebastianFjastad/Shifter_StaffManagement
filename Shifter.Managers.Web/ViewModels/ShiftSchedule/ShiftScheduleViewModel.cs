using Framework.CustomTypes;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.Helpers;
using Shifter.Shared.WebClient.ViewModels;
using System;
using System.Collections.Generic;

namespace Shifter.Managers.Web.ViewModels
{
    public class ShiftScheduleViewModel : ShifterModelBase
    {
        #region Constructors

        public ShiftScheduleViewModel()
        {
            CurrentWeek = DateTime.Now.StartOfWeek();
        }

        #endregion

        #region Properties

        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }

        public IEnumerable<ShiftTimeSlotDto> TimeSlots { get; set; }

        public DateTime CurrentWeek { get; set; }

        #endregion

        public DateTime GetWeekDate(DayOfWeekStartingAtMonday day, int weekNumber)
        {
            return Dates.GetDateFromDayAndWeek((int)day, weekNumber, CurrentWeek);
        }
    }
}