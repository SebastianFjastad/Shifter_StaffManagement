using System;
using Framework;
using Framework.Domain;
using Framework.Encryption;

namespace Shifter.Managers.Domain.Models
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
        /// Generates a new salt and encrypts the password 
        /// </summary>
        public void EncrypPassword(IPasswordEncryptor passwordEncryptor)
        {
            Guard.ArgumentNotNull(passwordEncryptor, "passwordEncryptor");
            Guard.InstanceNotNull(Password, "Password");

            Salt = passwordEncryptor.GenerateSalt();
            Password = Password.ComputePBKDF2Hash(Salt, passwordEncryptor);
        }

        public static UserAccount Create(string username, string password)
        {
            return new UserAccount
            {
                Username = username,
                Password = password
            };
        }

        #endregion
    }
}
