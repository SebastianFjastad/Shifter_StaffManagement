using System;
using Framework;
using Framework.Domain;
using Framework.Email;
using Framework.Encryption;
using Framework.Notifications;
using Shifter.Managers.Domain.Messages;
using Shifter.Managers.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Managers.Domain.Services
{
    public class WaiterService : IWaiterService
    {
        #region Constructors

        public WaiterService(IRepository repository, IEmailManager emailManager, IPasswordEncryptor passwordEncryptor, IPasswordGenerator passwordGenerator)
        {
            Guard.ArgumentNotNull(repository, "repository");
            Guard.ArgumentNotNull(emailManager, "emailManager");
            Guard.ArgumentNotNull(passwordEncryptor, "passwordEncryptor");
            Guard.ArgumentNotNull(passwordGenerator, "passwordGenerator");

            this.repository = repository;
            this.emailManager = emailManager;
            this.passwordEncryptor = passwordEncryptor;
            this.passwordGenerator = passwordGenerator;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;
        private readonly IEmailManager emailManager;
        private readonly IPasswordEncryptor passwordEncryptor;
        private readonly IPasswordGenerator passwordGenerator;

        #endregion

        #region Implementation of IWaiterService

        public IEnumerable<Waiter> LoadWaiters(int restaurantId)
        {
            return repository.FindBy<Waiter>(w => w.Restaurants.Any(r => r.Id == restaurantId));
        }

        public Waiter LoadWaiter(int waiterId)
        {
            return repository.FindById<Waiter>(waiterId);
        }

        public NotificationCollection SaveWaiter(Waiter waiter)
        {
            var newPassword = string.Empty;

            if (waiter.IsTransient())
            {
                newPassword = passwordGenerator.NewPassword();
                waiter.UserAccount = UserAccount.Create(waiter.EmailAddress, newPassword);
                waiter.UserAccount.EncrypPassword(passwordEncryptor);
            }
            else
            {
                var existingAccount = repository.FindById<Waiter>(waiter.Id);
                waiter.UserAccount = existingAccount.UserAccount;
                //Saving a waiter must not overwrite their shifts with null
                waiter.Shifts = existingAccount.Shifts;
            }

            var result = repository.Save(waiter);

            //If a new waiter was successfully saved
            if (!result.HasErrors() && newPassword.IsNotNullOrEmpty())
            {
                var msg = string.Format("You have been registered on Shifter. Your password has been set to {0}. You should change it to something you will remember after logging in.", newPassword);
                //result += EmailManager.SendEmail(waiter.EmailAddress, Config.FromEmailAddress, "Shifter registration", msg);
                MessagePublisher.PublishComsMessage(msg, "Shifter registration", waiter.EmailAddress);
            }

            return result;
        }

        public NotificationCollection ResetPassword(int waiterId)
        {
            var waiter = repository.FindById<Waiter>(waiterId);

            var newPassword = passwordGenerator.NewPassword();
            waiter.UserAccount.Password = newPassword;
            waiter.UserAccount.EncrypPassword(passwordEncryptor);

            var result = repository.Save(waiter);

            if (!result.HasErrors())
            {
                var msg = string.Format("Your password has been reset to {0}. You should change it to something you will remember after logging in.", newPassword);
                result += emailManager.SendEmail(waiter.EmailAddress, Config.FromEmailAddress, "Shifter registration", msg);
            }

            return result;
        }

        public NotificationCollection DeleteWaiter(int waiterId)
        {
            return repository.Delete<Waiter>(waiterId);
        }

        #endregion
    }
}
