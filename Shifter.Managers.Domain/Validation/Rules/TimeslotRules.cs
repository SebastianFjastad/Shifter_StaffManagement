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
    /// Waiter Rule: Timeslot start and end time are required
    /// </summary>
    [Rule(typeof (ShiftTimeslot))]
    [RuleContext(ValidationContextKeys.Save)]
    public class TR001 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var timeslot = entity as ShiftTimeslot;

                if (timeslot.StartTime.IsNull() || timeslot.EndTime.IsNull())
                {
                    notifications.AddError("Timeslot start time and end time are required.");
                }

                var sameTimeslots =
                    repository.FindBy<ShiftTimeslot>(
                        w => w.StartTime == timeslot.StartTime && w.EndTime == timeslot.EndTime);

                if (sameTimeslots.Any())
                {
                    notifications.AddError("Timeslot already exists.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Waiter Rule: Timeslot must have unique start and end times
    /// </summary>
    [Rule(typeof(ShiftTimeslot))]
    [RuleContext(ValidationContextKeys.Save)]
    public class TR002 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var timeslot = entity as ShiftTimeslot;

                var sameTimeslots =
                    repository.FindBy<ShiftTimeslot>(
                        t => t.StartTime == timeslot.StartTime && t.RestaurantId == timeslot.RestaurantId && t.EndTime == timeslot.EndTime);

                if (sameTimeslots.Any())
                {
                    notifications.AddError("Timeslot already exists.");
                }
            }

            return notifications;
        }
    }
}