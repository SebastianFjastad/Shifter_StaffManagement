using System;
using Framework;
using Framework.Domain;
using Framework.Email;
using Framework.Encryption;
using Framework.Notifications;
using Shifter.Waiters.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Waiters.Domain.Services
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

        public Waiter LoadWaiter(string username, string password)
        {
            var waiter = repository.FindBy<Waiter>(m => m.UserAccount.Username == username).FirstOrDefault();

            if (waiter.IsNotNull())
            {
                var passwordMatch = waiter.UserAccount.DoPasswordsMatch(password, passwordEncryptor);
                
                if (!passwordMatch)
                {
                    //Dont return the waiter if the password doesn't match
                    return null;
                }
            }

            return waiter;
        }

        public NotificationCollection SaveWaiter(Waiter waiter)
        {
            if (waiter.UserAccount.Password.IsNotNullOrEmpty())
            {
                waiter.UserAccount.EncrypPassword(passwordEncryptor);
            }

           var result = repository.Save(waiter);

           return result;
        }

        public NotificationCollection ResetPassword(int waiterId)
        {
            var result = NotificationCollection.CreateEmpty();

            var waiter = repository.FindById<Waiter>(waiterId);

            var newPassword = passwordGenerator.NewPassword();

            waiter.UserAccount.Password = newPassword;
            waiter.UserAccount.EncrypPassword(passwordEncryptor);

            result += repository.Save(waiter);

            if (result.HasErrors() == false)
            {
                var msg = string.Format("Your password has been reset to {0}. You should change it to something you will remember after logging in.", newPassword);

                result += emailManager.SendEmail(waiter.EmailAddress, "shahn.middleton@gmail.com", "Shifter reset password", msg);
            }

            return result;
        }

        public Waiter FindWaiter(string username)
        {
            var waiter = repository.FindBy<Waiter>(w => w.UserAccount.Username == username).FirstOrDefault();

            return waiter;
        }

        #endregion
    }
}
