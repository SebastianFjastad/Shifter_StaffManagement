using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IShiftTimeSlotDomainService
    {
        ShiftTimeslot LoadTimeSlot(int timeslotId);

        IEnumerable<ShiftTimeslot> LoadTimeSlots(int restaurantId, int? staffTypeId);

        NotificationCollection SaveTimeslot(ShiftTimeslot timeslot);

        NotificationCollection DeleteTimeslot(int timeslotId);
    }
}
