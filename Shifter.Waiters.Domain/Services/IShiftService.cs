using System;
using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain.Services
{
    public interface IShiftService
    {
        IEnumerable<Shift> LoadShifts(int restaurantId, DateTime? fromDate, DateTime? toDate, int? waiterId = null);

        IEnumerable<Shift> LoadShifts(int restaurantId, DateTime date, bool isAvailable);

        Shift LoadShift(int shiftId);

        NotificationCollection AssignShift(int waiterId, int shiftId, string clientKey);

        NotificationCollection UpdateShiftAvailablity(int shiftId, bool makeAvailable, string clientKey);
    }
}
