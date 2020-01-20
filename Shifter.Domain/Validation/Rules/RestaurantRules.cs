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
    [Rule(typeof(Restaurant))]
    [RuleContext(ValidationContextKeys.Save)]
    public class RR001 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var restaurant = entity as Restaurant;

                if (restaurant.Name.IsNullOrEmpty())
                {
                    notifications.AddError("The company name is required.");
                }
            }

            return notifications;
        }

        private Shift GetShiftsByWaiterRestaurantDateAndTime(IRepository repository, Shift shift)
        {
            return repository.FindBy<Shift>(s => s.Restaurant.Id == shift.Restaurant.Id && s.Staff.Id == shift.Staff.Id && s.IsCancelled == false && s.StartTime == shift.StartTime).FirstOrDefault();
        }
    }
}