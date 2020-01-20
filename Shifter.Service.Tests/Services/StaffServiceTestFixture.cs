using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Services;
using Shifter.Service.Services;

namespace Shifter.Service.Tests.Services
{
    [TestClass]
    public class StaffServiceTestFixture
    {
        [TestMethod]
        public void EnsureWaiterHoursAreCalculatedCorrectly()
        {
            var staffDomainService = A.Fake<IStaffDomainService>();
            var shiftService = A.Fake<IShiftDomainService>();

            var waiter = new Staff() {Id = 1, UserAccount = new UserAccount()};
            var shifts = new List<Shift>()
                         {
                             new Shift() {Staff = waiter, StartTime = DateTime.Today.Add(new TimeSpan(10, 30, 0)), EndTime = DateTime.Today.Add(new TimeSpan(13, 45, 0))},
                             new Shift() {Staff = waiter, StartTime = DateTime.Today.Add(new TimeSpan(12, 0, 0)), EndTime = DateTime.Today.Add(new TimeSpan(24, 0, 0))},
                             new Shift() {Staff = waiter, StartTime = DateTime.Today.Add(new TimeSpan(23, 0, 0)), EndTime = DateTime.Today.Add(new TimeSpan(25, 0, 0))},
                         };

            A.CallTo(() => shiftService.LoadShifts(1, null, DateTime.Now, DateTime.Now, null, null, null, null)).WithAnyArguments().Returns(shifts);
            A.CallTo(() => staffDomainService.LoadStaff(1, new List<int>())).WithAnyArguments().Returns(new List<Staff>() { waiter });

            var service = new StaffService(staffDomainService, shiftService, A.Fake<IRestaurantDomainService>());

            var hours = service.LoadStaffHours(new LoadStaffHoursRequest());

            Assert.IsTrue(hours.StaffHoursCollection.Any());
            Assert.IsTrue(hours.StaffHoursCollection.First().HoursWorked == new TimeSpan(17, 15, 0));
        }

        [TestMethod]
        public void EnsureShiftAssignmentNotificationsAreCreated()
        {
            var waiterService = A.Fake<IStaffDomainService>();
            var shiftService = A.Fake<IShiftDomainService>();

            var assignments = new List<TrackedShiftAssignment>()
                              {
                                  new TrackedShiftAssignment() {Id = 1, ShiftId = 1, StaffId = 5},
                                  new TrackedShiftAssignment() {Id = 2, ShiftId = 2, StaffId = 6},
                                  new TrackedShiftAssignment() {Id = 3, ShiftId = 9, StaffId = 9}
                              };

            var end = DateTime.Now.AddHours(2);
            var start = DateTime.Now.AddHours(1);

            A.CallTo(() => shiftService.LoadShiftAssignments(1, 5)).Returns(assignments);
            A.CallTo(() => shiftService.LoadShift(1)).Returns(new Shift() {Staff = new Staff() {Id = 5}, EndTime = end, StartTime = start});
            A.CallTo(() => shiftService.LoadShift(2)).Returns(new Shift() { Staff = new Staff() { Id = 1 }, EndTime = end, StartTime = start });
            A.CallTo(() => shiftService.LoadShift(9)).Returns(new Shift() { Staff = null, EndTime = end, StartTime = start });

            var service = new StaffService(waiterService, shiftService, A.Fake<IRestaurantDomainService>());

            var result = service.LoadStaffNotifications(new LoadByStaffAndRestaurantRequest(5, 1));

            Assert.IsTrue(result.NotificationCollection.HasMessages());
            Assert.IsTrue(result.NotificationCollection.Any(n => n.Text == string.Format("The shift on {0} at {1} is now assigned to you.", start.ToString(SharedConstants.DateFormat), start.ToString(SharedConstants.DateTimeSpecificTimeFormat)) && n.Tag.ToString() == "1"));
            Assert.IsTrue(result.NotificationCollection.Any(n => n.Text == string.Format("The shift on {0} at {1} is no longer assigned to you.", start.ToString(SharedConstants.DateFormat), start.ToString(SharedConstants.DateTimeSpecificTimeFormat)) && n.Tag.ToString() == "2"));
            Assert.IsTrue(result.NotificationCollection.Any(n => n.Text == string.Format("The shift on {0} at {1} is no longer assigned to you.", start.ToString(SharedConstants.DateFormat), start.ToString(SharedConstants.DateTimeSpecificTimeFormat)) && n.Tag.ToString() == "3"));
        }
    }
}
