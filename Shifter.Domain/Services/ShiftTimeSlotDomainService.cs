using System;
using System.Collections.Generic;
using Framework.Domain;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public class ShiftTimeSlotDomainService : IShiftTimeSlotDomainService
    {
        #region Constructors

        public ShiftTimeSlotDomainService(IRepository repository)
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

        public IEnumerable<ShiftTimeslot> LoadTimeSlots(int restaurantId, int? staffTypeId)
        {
            if (staffTypeId.IsNotNull())
            {
                return repository.FindBy<ShiftTimeslot>(s => s.RestaurantId == restaurantId && s.StaffTypeId == staffTypeId);
            }
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
