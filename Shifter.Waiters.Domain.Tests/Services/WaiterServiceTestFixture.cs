using System;
using System.Collections.Generic;
using FakeItEasy;
using Framework.Domain;
using Framework.Email;
using Framework.Encryption;
using NUnit.Framework;
using Shifter.Waiters.Domain.Models;
using Shifter.Waiters.Domain.Services;

namespace Shifter.Waiters.Domain.Tests.Services
{
    [TestFixture]
    public class WaiterServiceTestFixture
    {
        private WaiterService waiterService;
        private IPasswordEncryptor passwordEncryptor;
        private IPasswordGenerator passwordGenerator;
        private IRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = A.Fake<IRepository>();
            var emailManager = A.Fake<IEmailManager>();
            passwordEncryptor = A.Fake<IPasswordEncryptor>();
            passwordGenerator = A.Fake<IPasswordGenerator>();

            waiterService = new WaiterService(repository, emailManager, passwordEncryptor, passwordGenerator);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureWaiterServiceThrowsExceptionIfRepositoryIsNull()
        {
            new WaiterService(null, A.Fake<IEmailManager>(), A.Fake<IPasswordEncryptor>(), A.Fake<IPasswordGenerator>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureWaiterServiceThrowsExceptionIfEmailManagerIsNull()
        {
            new WaiterService(A.Fake<IRepository>(), null, A.Fake<IPasswordEncryptor>(), A.Fake<IPasswordGenerator>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureWaiterServiceThrowsExceptionIfPasswordEncryptorIsNull()
        {
            new WaiterService(A.Fake<IRepository>(), A.Fake<IEmailManager>(), null, A.Fake<IPasswordGenerator>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureWaiterServiceThrowsExceptionIfPasswordGeneratorIsNull()
        {
            new WaiterService(A.Fake<IRepository>(), A.Fake<IEmailManager>(), A.Fake<IPasswordEncryptor>(), null);
        }

        [Test]
        public void CanLoadWaiterIfPasswordsMatch()
        {
            const string salt = "salt";
            const string password = "abc";

            A.CallTo(() => passwordEncryptor.GenerateSalt()).Returns(salt);
            A.CallTo(() => passwordEncryptor.GenerateHash(password, salt)).Returns("cba");

            var waiter = new Waiter
            {
                UserAccount = new UserAccount
                {
                    Password = password.ComputePBKDF2Hash(salt, passwordEncryptor), 
                    Salt = salt
                }
            };

            A.CallTo(() => repository.FindBy<Waiter>(a => a.UserAccount.Username == "a"))
                .WithAnyArguments()
                .Returns(new List<Waiter> {waiter});

            var result = waiterService.LoadWaiter("a", password);

            Assert.IsTrue(result.IsNotNull());
        }

        [Test]
        public void EnsureCantLoadWaiterIfPasswordsDontMatch()
        {
            const string salt = "salt";
            const string password = "abc";

            A.CallTo(() => passwordEncryptor.GenerateSalt()).Returns(salt);
            A.CallTo(() => passwordEncryptor.GenerateHash(password, salt)).Returns("cba");

            var waiter = new Waiter
            {
                UserAccount = new UserAccount
                {
                    Password = password.ComputePBKDF2Hash(salt, passwordEncryptor),
                    Salt = salt
                }
            };

            A.CallTo(() => repository.FindBy<Waiter>(a => a.UserAccount.Username == "a"))
                .WithAnyArguments()
                .Returns(new List<Waiter> {waiter});

            var result = waiterService.LoadWaiter("a", "xyz");

            Assert.IsTrue(result.IsNull());
        }

        [Test]
        public void EnsureResetPasswordInvokesPasswordGeneratorNewPasswordOperation()
        {
            var waiter = new Waiter
            {
                UserAccount = new UserAccount
                {
                    Password = "abc",
                    Salt = "salt"
                }
            };

            A.CallTo(() => repository.FindById<Waiter>(1))
                .Returns(waiter);

            waiterService.ResetPassword(1);

            A.CallTo(() => passwordGenerator.NewPassword())
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
