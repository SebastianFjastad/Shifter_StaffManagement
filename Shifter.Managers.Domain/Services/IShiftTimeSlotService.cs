using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Services
{
    public interface IShiftTimeSlotService
    {
        ShiftTimeslot LoadTimeSlot(int timeslotId);

        IEnumerable<ShiftTimeslot> LoadTimeSlots(int restaurantId);

        NotificationCollection SaveTimeslot(ShiftTimeslot timeslot);

        NotificationCollection DeleteTimeslot(int timeslotId);
    }
}
