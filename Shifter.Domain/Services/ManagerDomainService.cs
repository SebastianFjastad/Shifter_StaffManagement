using System;
using System.Linq;
using Framework;
using Framework.Domain;
using Framework.Email;
using Framework.Encryption;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public class ManagerDomainService: IManagerDomainService
    {
        private readonly IPasswordEncryptor passwordEncryptor;
        private readonly IPasswordGenerator passwordGenerator;
        private readonly IRepository repository;

        #region Constructors

        public ManagerDomainService(IRepository repository, IPasswordEncryptor passwordEncryptor, IPasswordGenerator passwordGenerator)
        {
            Guard.ArgumentNotNull(repository, "repository");
            Guard.ArgumentNotNull(passwordEncryptor, "passwordEncryptor");
            Guard.ArgumentNotNull(passwordGenerator, "passwordGenerator");

            this.repository = repository;
            this.passwordEncryptor = passwordEncryptor;
            this.passwordGenerator = passwordGenerator;
        }

        #endregion

        public Manager LoadManager(string username, string password)
        {
            Guard.ArgumentNotNull(username, "username");
            Guard.ArgumentNotNull(password, "password");

            var manager = repository.FindBy<Manager>(m => m.UserAccount.Username == username).FirstOrDefault();

            if (manager.IsNotNull())
            {
                var passwordMatch = manager.UserAccount.DoPasswordsMatch(password, passwordEncryptor);

                if (!passwordMatch)
                {
                    //Dont return the waiter if the password doesn't match
                    return null;
                }
            }

            return manager;
        }

        public Manager LoadManager(int managerId)
        {
            return repository.FindById<Manager>(managerId);
        }

        public NotificationCollection ResetPassword(int managerId)
        {
            var manager = repository.FindById<Manager>(managerId);

            var newPassword = passwordGenerator.NewPassword();
            manager.UserAccount.Password = newPassword;
            manager.UserAccount.EncrypPassword(passwordEncryptor);

            var result = repository.Save(manager);

            if (!result.HasErrors())
            {
                var message = repository.FindBy<EmailTemplate>(e => e.TemplateName == Constants.EmailTemplates.PasswordReset).FirstOrDefault();

                message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Password, newPassword);
                message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Name, manager.UserAccount.FirstName);
                message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Username, manager.UserAccount.Username);

                result += repository.Save(EmailNotification.Create(message.Subject, message.Body, SharedConfig.FromSupportEmailAddress, manager.UserAccount.EmailAddress));
            }

            return result;
        }

        public Manager FindManager(string username)
        {
            Guard.ArgumentNotNull(username, "username");

            return repository.FindBy<Manager>(m => m.UserAccount.Username == username).FirstOrDefault();
        }

        public NotificationCollection UpdatePassword(int managerId, string password)
        {
            var manager = repository.FindById<Manager>(managerId);

            manager.UserAccount.Password = password;
            manager.UserAccount.EncrypPassword(passwordEncryptor);

            var result = repository.Save(manager);

            if (!result.HasErrors())
            {
                result += new Notification("Password reset successful.", NotificationSeverity.Information);
            }

            return result;
        }

        public NotificationCollection SaveManager(Manager manager)
        {
            var isNewManager = manager.IsTransient();
            var newPassword = string.Empty;

            var userAccount = manager.UserAccount;

            if (isNewManager)
            {
                //Reactivate the old account
                var inactiveAccount = repository.FindBy<Manager>(w => w.UserAccount.EmailAddress == manager.UserAccount.EmailAddress).FirstOrDefault();
                if (inactiveAccount.IsNotNull())
                {
                    manager.Id = inactiveAccount.Id;
                }

                newPassword = manager.UserAccount.Password.IsNotNullOrEmpty() ? manager.UserAccount.Password : passwordGenerator.NewPassword();
                manager.UserAccount = UserAccount.Create(manager.UserAccount.Username, newPassword, passwordEncryptor);
            }
            else
            {
                //Update info that must not be overwritten
                var existingAccount = repository.FindById<Manager>(manager.Id);
                manager.UserAccount = existingAccount.UserAccount;
            }

            manager.UserAccount.UpdatePersonalDetails(userAccount);

            var result = repository.Save(manager);

            if (!result.HasErrors())
            {
                result += new Notification("Saved successful.", NotificationSeverity.Information);

                if (isNewManager)
                {
                    var message = repository.FindBy<EmailTemplate>(e => e.TemplateName == Constants.EmailTemplates.ShifterRegistration).FirstOrDefault();

                    message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Password, newPassword);
                    message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Name, manager.UserAccount.FirstName);
                    message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Username, manager.UserAccount.Username);

                    result += repository.Save(EmailNotification.Create(message.Subject, message.Body, SharedConfig.FromSupportEmailAddress, manager.UserAccount.EmailAddress));
                }
            }

            return result;
        }
    }
}
