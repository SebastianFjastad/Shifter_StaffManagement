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
    /// StaffUnavailabilityRules Rule: Ensure staff unavailability is not saved for dates they are already working
    /// </summary>
    [Rule(typeof(StaffUnavailabilityRecord))]
    [RuleContext(ValidationContextKeys.Save)]
    public class SUR001 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var record = entity as StaffUnavailabilityRecord;
                var shiftsOnUnavailabilityDates = repository.FindBy<Shift>(s => s.Staff.Id == record.StaffId && (s.StartTime >= record.UnavailableFrom && s.StartTime <= record.UnavailableTo)); //TODO ensure it covers all combinations
                if (shiftsOnUnavailabilityDates.Any())
                {
                    notifications.AddError("Sorry you are already working during these dates.", ClientErrorCodes.Staff);
                    notifications.AddError("Sorry the staff member is already working during these dates.", ClientErrorCodes.Managers);
                }
            }

            return notifications;
        }
    }
}