using Framework.Domain;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Framework.PubSub;
using LinqKit;
using Shifter.Persistence;
using Shifter.Persistence.Data;
using Shifter.Waiters.Domain.Messages;
using Shifter.Waiters.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shifter.Waiters.Domain.Services
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

        //TODO use topic in domain
        string topic = "ShiftUpdated";

        #endregion

        #region Implementation of IShiftService

        public IEnumerable<Shift> LoadShifts(int restaurantId, DateTime? fromDate, DateTime? toDate, int? waiterId)
        {
            var predicate = BuildPredicate(restaurantId, fromDate, toDate, waiterId);

            return repository.FindBy<Shift>(predicate);
        }

        public IEnumerable<Shift> LoadShifts(int restaurantId, DateTime date, bool isAvailable)
        {
            var predicate = BuildPredicate(restaurantId, date, isAvailable);

            return repository.FindBy<Shift>(predicate);
        }

        public Shift LoadShift(int shiftId)
        {
            return repository.FindById<Shift>(shiftId);
        }

        public NotificationCollection AssignShift(int waiterId, int shiftId, string clientKey)
        {
            var notifications = NotificationCollection.CreateEmpty();

            var shift = repository.FindById<Shift>(shiftId);
            var waiter = repository.FindById<Waiter>(waiterId);

            if (shift.IsNull())
            {
                notifications.AddError("Shift does not exist.");
            }
            
            if (waiter.IsNull())
            {
                notifications.AddError("Waiter does not exist.");
            }

            if (!notifications.HasErrors())
            {
                shift.Waiter = waiter;
                shift.IsAvailable = false;

                notifications += repository.Save(shift);
                
                MessagePublisher.PublishApplicationMessage(topic, shift.Id, clientKey);
            }

            return notifications;
        }

        public NotificationCollection UpdateShiftAvailablity(int shiftId, bool makeAvailable, string clientKey)
        {
            var notifications = NotificationCollection.CreateEmpty();

            var shift = repository.FindById<Shift>(shiftId);

            if (shift.IsNull())
            {
                notifications.AddError("Shift does not exist.");
            }

            if (!notifications.HasErrors())
            {
                shift.IsAvailable = makeAvailable;

                notifications += repository.Save(shift);

                MessagePublisher.PublishApplicationMessage(topic, shift.Id, clientKey);
            }

            return notifications;
        }

        #endregion

        #region Private Methods

        private Expression<Func<Shift, bool>> BuildPredicate(int restaurantId, DateTime? fromDate, DateTime? toDate, int? waiterId)
        {
            var predicate = PredicateBuilder.True<Shift>();
            predicate.And(s => s.RestaurantId == restaurantId);

            if (waiterId.HasValue)
            {
                predicate = predicate.And(s => s.Waiter.Id == waiterId);
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

        private Expression<Func<Shift, bool>> BuildPredicate(int restaurantId, DateTime? date, bool isAvailable)
        {
            var predicate = PredicateBuilder.True<Shift>();
            predicate.And(s => s.RestaurantId == restaurantId);

            if (date.HasValue)
            {
                predicate = predicate.And(s => s.Date == date);
            }

            predicate = predicate.And(s => s.IsAvailable == isAvailable);

            return predicate;
        }

        #endregion
    }
}
