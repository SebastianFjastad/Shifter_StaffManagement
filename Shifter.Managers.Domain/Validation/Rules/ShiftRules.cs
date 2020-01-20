using System;
using System.Linq;
using Framework.Domain;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Managers.Domain.Models;
using Shifter.Persistence.Rules;

namespace Shifter.Managers.Domain.Validation.Rules
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

                if (shift.Waiter.IsNotNull())
                {
                    var existingShift = GetShiftsByWaiterRestaurantDateAndTime(repository, shift);

                    //are there any other shifts assigned to the same waiter, at the same restaurant, on the same date, at the same time
                    if (existingShift.IsNotNull() && existingShift.Id != shift.Id)
                    {
                        notifications.AddError("The waiter is already working a shift at this time.");
                    }
                }
            }

            return notifications;
        }

        private Shift GetShiftsByWaiterRestaurantDateAndTime(IRepository repository, Shift shift)
        {
            return repository.FindBy<Shift>(s => s.Restaurant.Id == shift.Restaurant.Id && s.Waiter.Id == shift.Waiter.Id && s.Date.Date == shift.Date.Date && s.StartTime == shift.StartTime).FirstOrDefault();
        }
    }
}