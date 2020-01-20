using System;
using Framework;
using Framework.Domain;
using Framework.Encryption;

namespace Shifter.Domain.Model.Entities
{
    public class UserAccount : Entity
    {
        #region Properties

        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string Salt { get; set; }


        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string ContactNumber { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the provided password matches the current encrypted password after it gets encrypted
        /// </summary>
        public bool DoPasswordsMatch(string password, IPasswordEncryptor passwordEncryptor)
        {
            var encryptedVersion = password.ComputePBKDF2Hash(Salt, passwordEncryptor);

            return Password == encryptedVersion;
        }

        /// <summary>
        /// Generates a new salt and encrypts the password 
        /// </summary>
        public void EncrypPassword(IPasswordEncryptor passwordEncryptor)
        {
            Guard.ArgumentNotNull(passwordEncryptor, "passwordEncryptor");
            Guard.InstanceNotNull(Password, "Password");

            Salt = passwordEncryptor.GenerateSalt();
            Password = Password.ComputePBKDF2Hash(Salt, passwordEncryptor);
        }

        public static UserAccount Create(string username, string password, IPasswordEncryptor passwordEncryptor)
        {
            var account = new UserAccount
            {
                Username = username,
                Password = password
            };

            account.EncrypPassword(passwordEncryptor);

            return account;
        }

        #endregion

        public void UpdatePersonalDetails(UserAccount userAccount)
        {
            FirstName = userAccount.FirstName;
            LastName = userAccount.LastName;
            EmailAddress = userAccount.EmailAddress;
            ContactNumber = userAccount.ContactNumber;
        }
    }
}
