using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Managers.Web.Actions;

namespace Shifter.Managers.Web.Tests.Actions.ShiftTemplate
{
    [TestClass]
    public class LoadShiftTemplateActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new LoadShiftTemplateAction<dynamic>(null);
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
                var action = new LoadShiftTemplateAction<dynamic>(A.Fake<IServiceRegistry>());

                action.Invoke(0);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnComplete"));
        }
    }
}
