using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.ViewModels;

namespace Shifter.Managers.Web.Tests.Actions
{
    [TestClass]
    public class SaveWaiterActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new SaveWaiterAction<dynamic>(null);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("serviceRegistry"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullOnSuccess()
        {
            var error = "";
            try
            {
                var action = new SaveWaiterAction<dynamic>(A.Fake<IServiceRegistry>())
                             {
                                 OnFailure = (m) => new { Value = m },
                             };

                action.Invoke(new StaffMemberViewModel(), 1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnSuccess"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullFailure()
        {
            var error = "";
            try
            {
                var action = new SaveWaiterAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                    OnSuccess = () => new {  },
                }; 

                action.Invoke(new StaffMemberViewModel(), 1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnFailure"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullViewModel()
        {
            var error = "";
            try
            {
                var action = new SaveWaiterAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                    OnFailure = (m) => new { Value = m },
                    OnSuccess = () => new { },
                };

                action.Invoke(null, 1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("viewModel"));
        }
    }
}
