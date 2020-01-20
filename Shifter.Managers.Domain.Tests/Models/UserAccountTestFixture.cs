using System;
using FakeItEasy;
using Framework.Encryption;
using NUnit.Framework;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Tests.Models
{
    [TestFixture]
    public class UserAccountTestFixture
    {
        [Test]
        public void EnsureEncryptPasswordGuardsAgainstNullPasswords()
        {
            var error = string.Empty;

            try
            {
                var account = new UserAccount();
                account.EncrypPassword(A.Fake<IPasswordEncryptor>());
            }
            catch(Exception x)
            {
                error = x.Message;
            }
            Assert.IsTrue(error.Contains("Password"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureEncryptPasswordTHrowsExceptionIfPasswordEncryptorIsNull()
        {
            var account = new UserAccount();

            account.EncrypPassword(null);
        }

        [Test]
        public void EnsureEncryptPasswordInvokesPasswordEncryptorGenerateSaltOperation()
        {
            var account = new UserAccount();
            account.Password = "123";
            var passwordEncryptor = A.Fake<IPasswordEncryptor>();

            account.EncrypPassword(passwordEncryptor);

            A.CallTo(() => passwordEncryptor.GenerateSalt())
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void EnsureEncryptPasswordInvokesPasswordEncryptorGenerateHashOperation()
        {
            const string salt = "salt";
            const string password = "123";            
            var account = new UserAccount();
            account.Password = password;
            var passwordEncryptor = A.Fake<IPasswordEncryptor>();
            A.CallTo(() => passwordEncryptor.GenerateSalt()).Returns(salt);

            account.EncrypPassword(passwordEncryptor);

            A.CallTo(() => passwordEncryptor.GenerateHash(password, salt))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
