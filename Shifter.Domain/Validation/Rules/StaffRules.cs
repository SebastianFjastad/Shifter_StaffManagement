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
    /// Staff Rule: Ensure staff has a valid email address (only if email not null)
    /// </summary>
    [Rule(typeof (Staff))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR001 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var staff = entity as Staff;

                if (staff.UserAccount.EmailAddress.IsNotNull() && !staff.UserAccount.EmailAddress.IsEmailAddress())
                {
                    notifications.AddError("Email address invalid.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Staff Rule: Ensure staff email address is unique
    /// </summary>
    [Rule(typeof(Staff))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR002 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Staff;

                var waitersWithSameEmail = repository.FindBy<Staff>(w => w.UserAccount.EmailAddress != null && w.UserAccount.EmailAddress == waiter.UserAccount.EmailAddress && w.Id != waiter.Id);

                if (waitersWithSameEmail.Any())
                {
                    notifications.AddError("Email address already exists.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Staff Rule: Ensure staff username is unique
    /// </summary>
    [Rule(typeof(Staff))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR007 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Staff;

                var existingUsername = repository.FindBy<UserAccount>(u => u.Username == waiter.UserAccount.Username && u.Id != waiter.UserAccount.Id);

                if (existingUsername.Any())
                {
                    notifications.AddError("This username is not available.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Staff Rule: Staff name is required
    /// </summary>
    [Rule(typeof(Staff))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR003 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Staff;

                if (waiter.UserAccount.FirstName.SafeTrim().IsNullOrEmpty() || waiter.UserAccount.LastName.SafeTrim().IsNullOrEmpty())
                {
                    notifications.AddError("Staff's first and last name are required.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Staff Rule: Staff contact number is required
    /// </summary>
    [Rule(typeof(Staff))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR004 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Staff;

                if (waiter.UserAccount.ContactNumber.SafeTrim().IsNullOrEmpty())
                {
                    notifications.AddError("Staff's contact number is required.");
                }
            }

            return notifications;
        }
    }

    /// <summary>
    /// Staff Rule: Cant save a new staff without a username and password
    /// </summary>
    [Rule(typeof(Staff))]
    [RuleContext(ValidationContextKeys.Save)]
    public class WR006 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var waiter = entity as Staff;

                if (waiter.UserAccount.IsNull() || waiter.UserAccount.Password.IsNullOrEmpty() || waiter.UserAccount.Username.IsNullOrEmpty())
                {
                    notifications.AddError("Staff must have an email address and password.");
                }
            }

            return notifications;
        }
    }
}