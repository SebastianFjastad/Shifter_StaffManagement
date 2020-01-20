using Framework.Domain;
using Framework.Notifications;
using Framework.PubSub;
using LinqKit;
using Shifter.Managers.Domain.Messages;
using Shifter.Managers.Domain.Models;
using Shifter.Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shifter.Managers.Domain.Services
{
    public class ShiftService : IShiftService
    {
        #region Constructors

        public ShiftService(IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        #region Implementation of IShiftService

        public IEnumerable<Shift> LoadShifts(int restaurantId, int? timeslotId, DateTime?  fromDate, DateTime? toDate)
        {
            var predicate = BuildPredicate(restaurantId, timeslotId, fromDate, toDate);

            return repository.FindBy<Shift>(predicate);
        }

        public Shift LoadShift(int shiftId)
        {
            return repository.FindById<Shift>(shiftId);
        }

        public NotificationCollection SaveShift(Shift shift)
        {
            var result = repository.Save(shift);
            if (!result.HasErrors())
            {
                MessagePublisher.PublishApplicationMessage(MessageTopics.ShiftUpdated, shift.Id);
            }

            return result;
        }

        public NotificationCollection DeleteShift(int shiftId)
        {
            return repository.Delete<Shift>(shiftId);
        }

        #endregion

        #region Private Methods

        private Expression<Func<Shift, bool>> BuildPredicate(int restaurantId, int? timeslotId, DateTime? fromDate, DateTime? toDate)
        {
            var predicate = PredicateBuilder.True<Shift>();
            predicate.And(s => s.Restaurant.Id == restaurantId);

            if (timeslotId.HasValue)
            {
                var timeSlot = repository.FindById<ShiftTimeslot>(timeslotId.Value);
                predicate = predicate.And(s => s.StartTime == timeSlot.StartTime && s.EndTime == timeSlot.EndTime);
            }

            if (fromDate.HasValue)
            {
                predicate = predicate.And(s => s.Date >= fromDate);
            }

            if (toDate.HasValue)
            {
                predicate = predicate.And(s => s.Date <= toDate);
            }
            return predicate;
        }

        #endregion
    }
}
