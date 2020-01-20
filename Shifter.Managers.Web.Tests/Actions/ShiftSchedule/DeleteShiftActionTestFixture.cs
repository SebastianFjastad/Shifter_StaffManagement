using System;
using System.Collections.Generic;
using FakeItEasy;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Managers.Web.Tests.Actions.ShiftSchedule
{
    [TestClass]
    public class DeleteShiftActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new DeleteShiftAction<dynamic>(null);
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
                var action = new DeleteShiftAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                };

                action.Invoke(0,0,0);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnComplete"));
        }


        [TestMethod]
        public void EnsureModelHasErrorsIfAny()
        {
            var system = A.Fake<IServiceRegistry>();
            A.CallTo(() => system.ShiftService.LoadShift(null)).WithAnyArguments().Returns(new LoadShiftResponse() {Shift = new ShiftDto()});
            A.CallTo(() => system.ShiftService.LoadShifts(null)).WithAnyArguments().Returns( new LoadShiftCollectionResponse(){Shifts = new List<ShiftDto>()});
            A.CallTo(() => system.ShiftService.DeleteShift(null)).WithAnyArguments().Returns(new GenericServiceResponse{NotificationCollection = NotificationCollection.CreateEmpty().AddError("error")
                         });

            var action = new DeleteShiftAction<dynamic>(system)
                         {
                             OnComplete = (m) => new {Model = m},
                         };

            var result = action.Invoke(0, 0, 0).Model as ShiftsResultViewModel;

            Assert.IsTrue(result.HasErrors);
        }
        
    }
}
