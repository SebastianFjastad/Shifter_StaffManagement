using FakeItEasy;
using Framework.Domain;
using Framework.Encryption;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using System;
using System.Collections.Generic;

namespace Shifter.Domain.Tests.Services
{
    [TestClass]
    public class StaffServiceTestFixture
    {
        private StaffDomainService staffDomainService;
        private IPasswordEncryptor passwordEncryptor;
        private IPasswordGenerator passwordGenerator;
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = A.Fake<IRepository>();
            passwordEncryptor = A.Fake<IPasswordEncryptor>();
            passwordGenerator = A.Fake<IPasswordGenerator>();

            staffDomainService = new StaffDomainService(repository, passwordEncryptor, passwordGenerator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureWaiterServiceThrowsExceptionIfRepositoryIsNull()
        {
            new StaffDomainService(null, A.Fake<IPasswordEncryptor>(), A.Fake<IPasswordGenerator>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureWaiterServiceThrowsExceptionIfPasswordEncryptorIsNull()
        {
            new StaffDomainService(A.Fake<IRepository>(), null, A.Fake<IPasswordGenerator>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsureWaiterServiceThrowsExceptionIfPasswordGeneratorIsNull()
        {
            new StaffDomainService(A.Fake<IRepository>(), A.Fake<IPasswordEncryptor>(), null);
        }

        [TestMethod]
        public void EnsureResetPasswordInvokesPasswordGeneratorNewPasswordOperation()
        {
            var waiter = new Staff
            {
                UserAccount = new UserAccount
                {
                    Password = "abc",
                    Salt = "salt"
                }
            };

            A.CallTo(() => repository.FindById<Staff>(1)).Returns(waiter);
            A.CallTo(() => repository.FindBy<EmailTemplate>(c => c == null)).WithAnyArguments().Returns(new List<EmailTemplate>() { new EmailTemplate(){Body = ""} });

            staffDomainService.ResetPassword(1);

            A.CallTo(() => passwordGenerator.NewPassword()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void CanLoadWaiterIfPasswordsMatch()
        {
            const string salt = "salt";
            const string password = "abc";

            A.CallTo(() => passwordEncryptor.GenerateSalt()).Returns(salt);
            A.CallTo(() => passwordEncryptor.GenerateHash(password, salt)).Returns("cba");

            var waiter = new Staff
            {
                UserAccount = new UserAccount
                {
                    Password = password.ComputePBKDF2Hash(salt, passwordEncryptor),
                    Salt = salt
                }
            };

            A.CallTo(() => repository.FindBy<Staff>(a => a.UserAccount.Username == "a"))
                .WithAnyArguments()
                .Returns(new List<Staff> { waiter });

            var result = staffDomainService.LoadStaff("a", password);

            Assert.IsTrue(result.IsNotNull());
        }

        [TestMethod]
        public void EnsureCantLoadWaiterIfPasswordsDontMatch()
        {
            const string salt = "salt";
            const string password = "abc";

            A.CallTo(() => passwordEncryptor.GenerateSalt()).Returns(salt);
            A.CallTo(() => passwordEncryptor.GenerateHash(password, salt)).Returns("cba");

            var waiter = new Staff
            {
                UserAccount = new UserAccount
                {
                    Password = password.ComputePBKDF2Hash(salt, passwordEncryptor),
                    Salt = salt
                }
            };

            A.CallTo(() => repository.FindBy<Staff>(a => a.UserAccount.Username == "a"))
                .WithAnyArguments()
                .Returns(new List<Staff> { waiter });

            var result = staffDomainService.LoadStaff("a", "xyz");

            Assert.IsTrue(result.IsNull());
        }

        [TestMethod]
        public void EnsureAddWaiterUpdatesUserAccount()
        {
            var newWaiter = new Staff()
            {
                CanWorkDoubles = true
            };

            var userAccount = new UserAccount()
            {
                FirstName = "First",
                LastName = "last",
                EmailAddress = "email",
                ContactNumber = "contact"
            };

            A.CallTo(() => repository.FindBy<EmailTemplate>(x => x == null)).WithAnyArguments().Returns(new List<EmailTemplate>() {new EmailTemplate() {Body = ""}});
            A.CallTo(() => repository.Save(new Staff())).WithAnyArguments().Returns(new NotificationCollection().AddError("fail"));

            staffDomainService.SaveStaff(newWaiter, userAccount);

            A.CallTo(() => repository.Save(new Staff())).WhenArgumentsMatch(s => s.Get<Staff>(0).UserAccount.EmailAddress == "email").MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void EnsureUpdateWaiterUpdatesUserAccount()
        {
            var newWaiter = new Staff()
            {
                Id = 1,
                CanWorkDoubles = true
            };

            var userAccount = new UserAccount()
            {
                FirstName = "First",
                LastName = "last",
                EmailAddress = "email",
                ContactNumber = "contact"
            };

            A.CallTo(() => repository.FindById<Staff>(1)).Returns(new Staff() { UserAccount = new UserAccount() { Id = 2 }, WelcomeEmailSent = true });

            staffDomainService.SaveStaff(newWaiter, userAccount);

            A.CallTo(() => repository.Save(new Staff())).WhenArgumentsMatch(s => s.Get<Staff>(0).UserAccount.FirstName == "First").MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void EnsureAddWaiterSetsIsActiveToTrue()
        {
            var newWaiter = new Staff()
            {
                CanWorkDoubles = true
            };

            var userAccount = new UserAccount()
            {
                FirstName = "First",
                LastName = "last",
                EmailAddress = "email",
                ContactNumber = "contact"
            };

            A.CallTo(() => repository.FindBy<EmailTemplate>(x => x == null)).WithAnyArguments().Returns(new List<EmailTemplate>() { new EmailTemplate() { Body = "" } });
            A.CallTo(() => repository.Save(new Staff())).WithAnyArguments().Returns(new NotificationCollection().AddError("fail"));

            staffDomainService.SaveStaff(newWaiter, userAccount);

            A.CallTo(() => repository.Save(new Staff())).WhenArgumentsMatch(s => s.Get<Staff>(0).IsActive).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void EnsureUpdateWaiterSetsIsActiveToTrue()
        {
            var newWaiter = new Staff()
            {
                Id = 1,
                CanWorkDoubles = true,
                IsActive = false
            };

            var userAccount = new UserAccount()
            {
                FirstName = "First",
            };

            A.CallTo(() => repository.FindById<Staff>(1)).Returns(new Staff() { UserAccount = new UserAccount() { Id = 2 }, WelcomeEmailSent = true });

            staffDomainService.SaveStaff(newWaiter, userAccount);

            A.CallTo(() => repository.Save(new Staff())).WhenArgumentsMatch(s => s.Get<Staff>(0).IsActive == false).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
