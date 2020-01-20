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
    /// Waiter Rule: Ensure waiter has a valid email address
    /// </summary>
    [Rule(typeof (Waiter))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR001 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var item = entity as Waiter;

                if (item.EmailAddress.IsNull() || !item.EmailAddress.IsEmailAddress())
                {
                    notifications.AddError("Email address invalid.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Waiter Rule: Ensure waiter email address is unique
    /// </summary>
    [Rule(typeof(Waiter))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR002 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Waiter;

                var waitersWithSameEmail = repository.FindBy<Waiter>(w => w.EmailAddress == waiter.EmailAddress && w.Id != waiter.Id);

                if (waitersWithSameEmail.Any())
                {
                    notifications.AddError("Email address already exists.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Waiter Rule: Waiter name is required
    /// </summary>
    [Rule(typeof(Waiter))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR003 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Waiter;

                if (waiter.FirstName.SafeTrim().IsNullOrEmpty() || waiter.LastName.SafeTrim().IsNullOrEmpty())
                {
                    notifications.AddError("Waiter's first and last name are required.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Waiter Rule: Waiter contact number is required
    /// </summary>
    [Rule(typeof(Waiter))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR004 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Waiter;

                if (waiter.ContactNumber.SafeTrim().IsNullOrEmpty())
                {
                    notifications.AddError("Waiter's contact number is required.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Waiter Rule: Cant delete waiter that has shifts in the future assigned to them
    /// </summary>
    [Rule(typeof(Waiter))]
    [RuleContext(ValidationContextKeys.Delete)]
    public class WR005 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Waiter;

                var today = DateTime.Now;

                var waiterShifts = repository.FindBy<Shift>(s => s.Waiter.Id == waiter.Id && s.Date >= today);

                if (waiterShifts.Any())
                {
                    notifications.AddError("Waiter cannot be deleted because there are still shifts assigned to them.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Waiter Rule: Cant save a new waiter without a username and password
    /// </summary>
    [Rule(typeof(Waiter))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR006 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Waiter;

                if (waiter.UserAccount.IsNull() || waiter.UserAccount.Password.IsNullOrEmpty() || waiter.UserAccount.Username.IsNullOrEmpty())
                {
                    notifications.AddError("Waiter must have an email address and password.");
                }
            }

            return notifications;
        }
    }
}