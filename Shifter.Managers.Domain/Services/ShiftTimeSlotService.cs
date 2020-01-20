using System.Collections.Generic;
using Framework.Domain;
using Framework.Notifications;
using Shifter.Managers.Domain.Models;
using Shifter.Persistence;

namespace Shifter.Managers.Domain.Services
{
    public class ShiftTimeSlotService : IShiftTimeSlotService
    {
        #region Constructors

        public ShiftTimeSlotService(IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        #region Implementation of IShiftTimeSlotService

        public ShiftTimeslot LoadTimeSlot(int timeslotId)
        {
            return repository.FindById<ShiftTimeslot>(timeslotId);
        }

        public IEnumerable<ShiftTimeslot> LoadTimeSlots(int restaurantId)
        {
            return repository.FindBy<ShiftTimeslot>(s => s.RestaurantId == restaurantId);
        }

        public NotificationCollection SaveTimeslot(ShiftTimeslot timeslot)
        {
            return repository.Save(timeslot);
        }

        public NotificationCollection DeleteTimeslot(int timeslotId)
        {
            return repository.Delete<ShiftTimeslot>(timeslotId);
        }

        #endregion
    }
}
