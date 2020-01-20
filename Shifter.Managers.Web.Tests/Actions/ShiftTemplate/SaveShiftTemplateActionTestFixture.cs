using System;
using FakeItEasy;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Managers.Web.Tests.Actions.ShiftTemplate
{
    [TestClass]
    public class SaveShiftTemplateActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new SaveShiftTemplateAction<dynamic>(null);
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
                var action = new SaveShiftTemplateAction<dynamic>(A.Fake<IServiceRegistry>());

                action.Invoke(new ShiftTemplateViewModel(), 0);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnComplete"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullViewModel()
        {
            var error = "";
            try
            {
                var action = new SaveShiftTemplateAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                    OnComplete = (m) => new { Value = true },
                };

                action.Invoke(null, 1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("viewModel"));
        }


        [TestMethod]
        public void EnsureViewModelHadErrorsIfSaveFailed()
        {
            var system = A.Fake<IServiceRegistry>();
            A.CallTo(() => system.ShiftTemplateService.SaveTemplates(null)).WithAnyArguments().
                Returns(new GenericServiceResponse
                        {
                            NotificationCollection = NotificationCollection.CreateEmpty().AddError("error")
                        });

            var action = new SaveShiftTemplateAction<dynamic>(system)
                         {
                             OnComplete = (m) => new {Value = m},
                         };

            var result = action.Invoke(new ShiftTemplateViewModel(), 1).Value as ShiftTemplateViewModel;

            Assert.IsTrue(result.Notifications.HasErrors());
        }
    }
}
