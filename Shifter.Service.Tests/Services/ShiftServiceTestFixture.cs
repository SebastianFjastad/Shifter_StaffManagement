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
    public class ShiftServiceTestFixture
    {
        [TestMethod]
        public void Ensure_CopyShifts_Deletes_Existing_If_Overwrite_True()
        {
            var shiftService = A.Fake<IShiftDomainService>();

            var service = new ShiftService(shiftService);


            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date.AddDays(6);
            var copyToStartDate = endDate.AddDays(1);

            A.CallTo(() => shiftService.LoadShifts(0, null, null, null, null, null, null, null)).WithAnyArguments().Returns(new List<Shift>() { new Shift() { StartTime = startDate } });

            var request = new CopyShiftsRequest()
            {
                CopyFromStartDate = startDate,
                CopyFromEndDate = endDate,
                CopyToStartDate = copyToStartDate,
                OverwriteExistingShifts = true
            };

            var result = service.CopyShifts(request);

            var expectedCopyToDate = endDate.AddDays(1);

            A.CallTo(() => shiftService.DeleteShifts(new List<Shift>())).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => shiftService.SaveShifts(new List<Shift>())).WhenArgumentsMatch(s => s.Get<List<Shift>>(0).First().StartTime == expectedCopyToDate).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void Ensure_CopyShifts_Doesnt_Deletes_Existing_If_Overwrite_False()
        {
            var shiftService = A.Fake<IShiftDomainService>();

            var service = new ShiftService(shiftService);


            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date.AddDays(6);
            var copyToStartDate = endDate.AddDays(1);

            A.CallTo(() => shiftService.LoadShifts(0, null, null, null, null, null, null, null)).WithAnyArguments().Returns(new List<Shift>() { new Shift() { StartTime = startDate } });

            var request = new CopyShiftsRequest()
                          {
                              CopyFromStartDate = startDate,
                              CopyFromEndDate = endDate,
                              CopyToStartDate = copyToStartDate,
                          };

            var result = service.CopyShifts(request);

            var expectedCopyToDate = endDate.AddDays(1);

            A.CallTo(() => shiftService.DeleteShifts(new List<Shift>())).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => shiftService.SaveShifts(new List<Shift>())).WhenArgumentsMatch(s => s.Get<List<Shift>>(0).First().StartTime == expectedCopyToDate).MustHaveHappened(Repeated.Exactly.Once);

        }
    }
}
