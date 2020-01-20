using Framework;
using Framework.Domain;
using Framework.Encryption;
using Framework.Notifications;
using Shifter.Domain.Model;
using Shifter.Domain.Model.Entities;
using System;
using System.Linq;

namespace Shifter.Domain.Services
{
    public class AuthenticationDomainService : IAuthenticationDomainService
    {
        private readonly IRepository repository;
        private readonly IPasswordEncryptor passwordEncryptor;
        private readonly IPasswordGenerator passwordGenerator;

        #region Constructors

        public AuthenticationDomainService(IRepository repository, IPasswordEncryptor passwordEncryptor, IPasswordGenerator passwordGenerator)
        {
            Guard.ArgumentNotNull(repository, "repository");
            Guard.ArgumentNotNull(passwordEncryptor, "passwordEncryptor");
            Guard.ArgumentNotNull(passwordGenerator, "passwordGenerator");

            this.repository = repository;
            this.passwordEncryptor = passwordEncryptor;
            this.passwordGenerator = passwordGenerator;
        }

        #endregion

        public UserAccount FindUserAccount(string username, string password)
        {
            Guard.ArgumentNotNull(username, "username");
            Guard.ArgumentNotNull(password, "password");

            var userAccount = repository.FindBy<UserAccount>(m => m.Username == username).FirstOrDefault();

            if (userAccount.IsNotNull())
            {
                var passwordMatch = userAccount.DoPasswordsMatch(password, passwordEncryptor);

                if (!passwordMatch)
                {
                    return null;
                }
            }

            return userAccount;
        }

        public UserAccount FindUserAccount(string username)
        {
            Guard.ArgumentNotNull(username, "username");

            var userAccount = repository.FindBy<UserAccount>(m => m.Username == username).FirstOrDefault();

            return userAccount;
        }

        public NotificationCollection SaveUserAccount(UserAccount userAccount)
        {
            var result = repository.Save(userAccount);

            return result;
        }

        public T LoadProfileByUserAccount<T>(int userAccountId) where T : IProfile
        {
            var profile = repository.FindBy<T>(p => p.UserAccount.Id == userAccountId).FirstOrDefault();

            return profile.IsNotNull() ? (profile) : default(T);
        }

        public UserAccount LoadUserAccount(int userAccountId)
        {
           var userAccount = repository.FindById<UserAccount>(userAccountId);

            return userAccount;
        }

        public NotificationCollection ResetPassword(int userAccountId)
        {
            var result = NotificationCollection.CreateEmpty();

            var userAccount = repository.FindById<UserAccount>(userAccountId);

            if (userAccount.IsNotNull())
            {
                var newPassword = passwordGenerator.NewPassword();
                userAccount.Password = newPassword;
                userAccount.EncrypPassword(passwordEncryptor);

                result += repository.Save(userAccount);

                if (!result.HasErrors())
                {
                    var message = repository.FindBy<EmailTemplate>(e => e.TemplateName == Constants.EmailTemplates.PasswordReset).FirstOrDefault();

                    message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Password, newPassword);
                    message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Name, userAccount.FirstName);
                    message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Username, userAccount.Username);

                    result += repository.Save(EmailNotification.Create(message.Subject, message.Body, SharedConfig.FromSupportEmailAddress, userAccount.EmailAddress));
                }
                if (!result.HasErrors())
                {
                    result += new Notification("Password reset successful.", NotificationSeverity.Information);
                }
            }

            return result;
        }

        public NotificationCollection SaveNewPassword(int userAccountId, string password)
        {
            var userAccount = repository.FindById<UserAccount>(userAccountId);

            userAccount.Password = password;
            userAccount.EncrypPassword(passwordEncryptor);

            var result = repository.Save(userAccount);

            if (!result.HasErrors())
            {
                result += new Notification("Password reset successful.", NotificationSeverity.Information);
            }

            return result;
        }
    }
}
