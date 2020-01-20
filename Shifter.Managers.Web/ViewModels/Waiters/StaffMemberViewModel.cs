using Framework.Notifications;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.Helpers;
using Shifter.Shared.WebClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Managers.Web.ViewModels
{
    public class StaffMemberViewModel : ShifterModelBase
    {
        #region Constructors

        public StaffMemberViewModel()
        {
            Staff = new StaffDto();
            Staff.Shifts = new List<ShiftDto>();
            LeaveErrors = NotificationCollection.CreateEmpty();
        }

        #endregion

        #region Properties

        public StaffDto Staff { get; set; }

        public bool EditMode { get; set; }

        public bool StaffMemberHasNoEmailAddress { get; set; }

        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }

        public bool ShowLeave { get; set; }

        public NotificationCollection LeaveErrors { get; set; }

        #endregion

        #region Methods

        public IEnumerable<ShiftDto> GetWaiterShifts(int day, int week)
        {
            var date = Dates.GetDateFromDayAndWeek(day, week);

            var shifts = Staff.Shifts.Where(s => s.StartTime.Date == date.Date);

            return shifts;
        }

        #endregion

        public bool HasShift(int day, int week)
        {
            var date = Dates.GetDateFromDayAndWeek(day, week);

            var shift = Staff.Shifts.FirstOrDefault(s => s.StartTime.Date == date.Date);

            return shift.IsNotNull();
        }
    }
}