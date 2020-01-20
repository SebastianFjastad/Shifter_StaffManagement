using System;

namespace Framework.Encryption
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string NewPassword()
        {
            var newPassword = Guid.NewGuid().ToString().Substring(0, 6);

            return newPassword;
        }
    }
}
