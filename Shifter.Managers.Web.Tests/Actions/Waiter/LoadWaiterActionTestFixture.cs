using System;
using System.Web.Security;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Domain.Model.Entities;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Managers.Web.Tests.Actions
{
    [TestClass]
    public class LoadWaiterActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new LoadStaffMemberAction<dynamic>(null);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("serviceRegistry"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullOnComplete()
        {
            var error = "";
            try
            {
                var action = new LoadStaffMemberAction<dynamic>(A.Fake<IServiceRegistry>());

                action.Invoke(1, 1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnComplete"));
        }


        [TestMethod]
        public void EnsureWaiterIsNotNullIfWaiterIdIsNull()
        {
            var system = A.Fake<IServiceRegistry>();

            var action = new LoadStaffMemberAction<dynamic>(system)
            {
                OnComplete = (m) => new { Value = m },
            };

            var result = action.Invoke(1, 1).Value as StaffMemberViewModel;

            Assert.IsNotNull(result.Staff);
            Assert.IsTrue(result.Notifications.HasErrors());
        }

        [TestMethod]
        public void EnsureWaiterIsLoadedAndWaitersAreLoadedIfWaiterIdIsNotNull()
        {
            var system = A.Fake<IServiceRegistry>();
            var waiter = new StaffDto { Id = 1, EmailAddress = "fake@email.com" };
            var user = A.Fake<MembershipUser>();

            A.CallTo(() => user.GetPassword()).Returns("pass");

            A.CallTo(() => system.StaffService.LoadStaffMember(null)).WithAnyArguments().Returns(new StaffResponse { Staff = waiter });

            var action = new LoadStaffMemberAction<dynamic>(system)
            {
                OnComplete = (m) => new { Value = m },
            };

            var result = action.Invoke(1, 1).Value as StaffMemberViewModel;


            Assert.IsTrue(result.Staff.Id == 1);
            Assert.IsFalse(result.Notifications.HasErrors());
        }

        [TestMethod]
        public void EnsureModelHasErrorsIfFailedToLoadWaiter()
        {
            var system = A.Fake<IServiceRegistry>();

            var genericWaiterRequest = new LoadStaffMemberRequest { StaffId = 1 };
            A.CallTo(() => system.StaffService.LoadStaffMember(genericWaiterRequest)).Returns(null);

            var action = new LoadStaffMemberAction<dynamic>(system)
            {
                OnComplete = (m) => new { Value = m },
            };

            var result = action.Invoke(1, 1).Value as StaffMemberViewModel;

            Assert.IsNotNull(result.Staff);
            Assert.IsTrue(result.Notifications.HasErrors());
        }
    }
}
