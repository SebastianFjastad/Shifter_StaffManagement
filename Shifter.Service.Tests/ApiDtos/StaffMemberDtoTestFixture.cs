using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Tests.ApiDtos
{
    [TestClass]
    public class StaffMemberDtoTestFixture
    {
        [TestMethod]
        public void Ensure_IsUnavailableOnDate_ReturnsTrueIfDateBetweenUnavailabilityPeriod()
        {
            var from = DateTime.Now.AddDays(1);
            var to = DateTime.Now.AddDays(5);

            var staffMember = new StaffDto(){UnavailabilityRecords = new List<StaffUnavailabilityRecordDto>{new StaffUnavailabilityRecordDto(){UnavailableFrom = from, UnavailableTo = to}}};
            var result = staffMember.IsUnavailableOnDate(from.AddDays(1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Ensure_IsUnavailableOnDate_ReturnsTrueIfDateOnStartDate()
        {
            var from = DateTime.Now.AddDays(1);
            var to = DateTime.Now.AddDays(5);

            var staffMember = new StaffDto() { UnavailabilityRecords = new List<StaffUnavailabilityRecordDto> { new StaffUnavailabilityRecordDto() { UnavailableFrom = from, UnavailableTo = to } } };
            var result = staffMember.IsUnavailableOnDate(from);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Ensure_IsUnavailableOnDate_ReturnsTrueIfDateOnEndDate()
        {
            var from = DateTime.Now.AddDays(1);
            var to = DateTime.Now.AddDays(5);

            var staffMember = new StaffDto() { UnavailabilityRecords = new List<StaffUnavailabilityRecordDto> { new StaffUnavailabilityRecordDto() { UnavailableFrom = from, UnavailableTo = to } } };
            var result = staffMember.IsUnavailableOnDate(to);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Ensure_IsUnavailableOnDate_ReturnsFalseIfDateBeforeStartDate()
        {
            var from = DateTime.Now.AddDays(1);
            var to = DateTime.Now.AddDays(5);

            var staffMember = new StaffDto() { UnavailabilityRecords = new List<StaffUnavailabilityRecordDto> { new StaffUnavailabilityRecordDto() { UnavailableFrom = from, UnavailableTo = to } } };
            var result = staffMember.IsUnavailableOnDate(DateTime.Now);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ensure_IsUnavailableOnDate_ReturnsFalseIfDateAfterEndDate()
        {
            var from = DateTime.Now.AddDays(1);
            var to = DateTime.Now.AddDays(5);

            var staffMember = new StaffDto() { UnavailabilityRecords = new List<StaffUnavailabilityRecordDto> { new StaffUnavailabilityRecordDto() { UnavailableFrom = from, UnavailableTo = to } } };
            var result = staffMember.IsUnavailableOnDate(to.AddDays(1));

            Assert.IsFalse(result);
        }
    }
}
