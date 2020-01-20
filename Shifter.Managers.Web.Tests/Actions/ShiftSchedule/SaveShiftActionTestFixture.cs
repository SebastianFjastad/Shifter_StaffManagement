using System;
using System.Collections.Generic;
using FakeItEasy;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Domain.Model.Entities;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Responses;

namespace Shifter.Managers.Web.Tests.Actions.ShiftSchedule
{
    [TestClass]
    public class SaveShiftActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new SaveShiftAction<dynamic>(null);
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
                var action = new SaveShiftAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                   
                };

                action.Invoke(new TimeSpan(), new TimeSpan(), DateTime.Now, 0, 0);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnComplete"));
        }


        [TestMethod]
        public void EnsureResultHasErrorsIfAny()
        {
            var system = A.Fake<IServiceRegistry>();
            A.CallTo(() => system.ShiftService.SaveShift(null)).WithAnyArguments()
                .Returns(new GenericServiceResponse
                {
                    NotificationCollection = NotificationCollection.CreateEmpty().AddError("error")
                });

            A.CallTo(() => system.ShiftService.LoadShifts(null)).WithAnyArguments().Returns(new LoadShiftCollectionResponse(){Shifts = new List<ShiftDto>()});

            var action = new SaveShiftAction<dynamic>(system)
            {
                OnComplete = (m) => new { Model = m },
            };

            var result = action.Invoke(new TimeSpan(), new TimeSpan(), DateTime.Now, 0, 0).Model as ShiftsResultViewModel;

            Assert.IsTrue(result.HasErrors);
        }
    }
}
