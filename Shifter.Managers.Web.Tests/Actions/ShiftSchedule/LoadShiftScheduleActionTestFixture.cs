using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Domain.Model.Entities;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Managers.Web.Tests.Actions.ShiftSchedule
{
    [TestClass]
    public class LoadShiftScheduleActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new LoadShiftScheduleAction<dynamic>(null);
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
                var action = new LoadShiftScheduleAction<dynamic>(A.Fake<IServiceRegistry>());

                action.Invoke(0, DateTime.Now, DateTime.Now, new List<int>());
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnComplete"));
        }

       
        [TestMethod]
        public void EnsureShiftsAreReturned()
        {
            var system = A.Fake<IServiceRegistry>();
            A.CallTo(() => system.ShiftService.LoadShifts(null)).WithAnyArguments().Returns(new LoadShiftCollectionResponse{Shifts = new List<ShiftDto> {new ShiftDto()}});

            A.CallTo(() => system.StaffService.LoadStaffList(new LoadStaffListRequest {RestaurantId = 1})).Returns(new StaffCollectionResponse {Staff = new List<StaffDto> {new StaffDto()}});

            var action = new LoadShiftScheduleAction<dynamic>(system)
            {
                OnComplete = (m) => new { Value = m },
            };

            var result = action.Invoke(1, DateTime.Now, DateTime.Now, new List<int>()).Value as ShiftScheduleResultsViewModel;

            Assert.IsNotNull(result.Shifts.Any());
        }
    }
}
