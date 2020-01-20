using System;
using System.Linq;
using Framework.Domain;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Framework.Rules;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Validation.Rules
{
    /// <summary>
    /// Waiter Rule: Ensure a waiter is not working more than one shift at the same time
    /// </summary>
    [Rule(typeof(Shift))]
    [RuleContext(ValidationContextKeys.Save)]
    public class SR001 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var shift = entity as Shift;

                if (shift.Staff.IsNotNull())
                {
                    var existingShift = GetShiftsByWaiterRestaurantDateAndTime(repository, shift);

                    //are there any other shifts assigned to the same waiter, at the same restaurant, on the same date, at the same time
                    if (existingShift.IsNotNull() && existingShift.Id != shift.Id)
                    {
                        notifications.AddError("The staff member is already working a shift at this time.");
                    }
                }
            }

            return notifications;
        }

        private Shift GetShiftsByWaiterRestaurantDateAndTime(IRepository repository, Shift shift)
        {
            return repository.FindBy<Shift>(s => s.Restaurant.Id == shift.Restaurant.Id && s.Staff.Id == shift.Staff.Id && s.IsCancelled == false && s.StartTime == shift.StartTime).FirstOrDefault();
        }
    }

    /// <summary>
    /// Shift Rule: Ensure a shift has a start time and end time
    /// </summary>
    [Rule(typeof(Shift))]
    [RuleContext(ValidationContextKeys.Save)]
    public class SR002 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var shift = entity as Shift;

                if (shift.StartTime.IsNull() || shift.EndTime.IsNull())
                {
                    notifications.AddError("A shift must have a start and end time.");
                }
            }

            return notifications;
        }
    }
}