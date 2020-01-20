using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Managers.Web.Actions;

namespace Shifter.Managers.Web.Tests.Actions
{
    [TestClass]
    public class DeleteWaiterActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new DeleteWaiterAction<dynamic>(null);
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
                var action = new DeleteWaiterAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                };

                action.Invoke(1, 1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnComplete"));
        }
    }
}
