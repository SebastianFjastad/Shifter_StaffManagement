using System;
using Framework;
using Framework.Domain;
using Framework.Encryption;

namespace Shifter.Waiters.Domain.Models
{
    public class UserAccount : Entity
    {
        #region Properties

        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string Salt { get; set; }

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

        #endregion
    }
}
