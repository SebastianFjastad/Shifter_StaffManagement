using System;
using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Services
{
    public interface IShiftService
    {
        IEnumerable<Shift> LoadShifts(int restaurantId, int? timeslotId, DateTime? fromDate, DateTime? toDate);

        Shift LoadShift(int shiftId);

        NotificationCollection SaveShift(Shift shift);

        NotificationCollection DeleteShift(int shiftId);
    }
}
