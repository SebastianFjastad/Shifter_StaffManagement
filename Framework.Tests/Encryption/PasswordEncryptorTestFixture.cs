using System;
using FakeItEasy;
using Framework.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.Encryption
{

    [TestClass]
    public class PasswordEncryptorTestFixture
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsurePasswordEncryptorThrowsExceptionWhenCryptoServiceIsNull()
        {
            new PasswordEncryptor(null);
        }

        [TestMethod]
        public void EnsureGenerateSaltInvokesCryptoServiceGenerateSaltOperation()
        {
            var cryptoService = A.Fake<ICryptoService>();

            var service = new PasswordEncryptor(cryptoService);

            service.GenerateSalt();

            A.CallTo(() => cryptoService.GenerateSalt())
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void EnsureGenerateHashInvokesCryptoServiceComputeOperation()
        {
            var cryptoService = A.Fake<ICryptoService>();

            var service = new PasswordEncryptor(cryptoService);

            const string salt = "salt";
            const string password = "password";

            service.GenerateHash(password, salt);

            A.CallTo(() => cryptoService.Compute(password, salt))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
