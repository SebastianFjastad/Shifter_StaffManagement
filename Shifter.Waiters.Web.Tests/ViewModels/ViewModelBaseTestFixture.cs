using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Service.Api.Dtos;
using Shifter.Waiters.Web.ViewModels;

namespace Shifter.Waiters.Web.Tests.ViewModels
{
    [TestClass]
    public class ViewModelBaseTestFixture : ViewModelBase
    {
        static readonly DateTime date = DateTime.Now.Date.AddDays(1);

        [TestMethod]
        public void EnsureCanWaiterWork_IfFalseIfShiftOverlapsAnExistingShift()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto(){StartTime = new DateTime(date.Year,date.Month,date.Day,14,00,00), EndTime = new DateTime(date.Year,date.Month,date.Day,15,00,00)}
            };

            var earlierShift = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 13, 30, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 14, 30, 00) };
            var latershift = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 14, 30, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 15, 30, 00) };

            var canWaiterWorkShiftStartingEalier = CanWaiterWorkThisShift(waiterShifts, earlierShift);
            var canWaiterWorkShiftStartingDuring = CanWaiterWorkThisShift(waiterShifts, latershift);

            Assert.IsFalse(canWaiterWorkShiftStartingEalier);
            Assert.IsFalse(canWaiterWorkShiftStartingDuring);
        }

        [TestMethod]
        public void EnsureCanWaiterWork_IfFalseIfShiftIsBetweenAnExistingShift()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto(){StartTime = new DateTime(date.Year,date.Month,date.Day,13,00,00), EndTime = new DateTime(date.Year,date.Month,date.Day,16,00,00)}
            };

            var shift = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 14, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 15, 00, 00) };

            var result = CanWaiterWorkThisShift(waiterShifts, shift);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EnsureCanWaiterWork_IfFalseIfAnExistingIsBwteenShiftToTake()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto(){StartTime = new DateTime(date.Year,date.Month,date.Day,14,00,00), EndTime = new DateTime(date.Year,date.Month,date.Day,15,00,00)}
            };

            var shift = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 13, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 16, 00, 00) };

            var result = CanWaiterWorkThisShift(waiterShifts, shift);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EnsureCanWaiterWork_IfTrueIfShiftToTakeEndsAsAnotherStarts()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto(){StartTime = new DateTime(date.Year,date.Month,date.Day,18,00,00), EndTime = new DateTime(date.Year,date.Month,date.Day,19,00,00)}
            };

            var shift = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 16, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 18, 00, 00) };

            var result = CanWaiterWorkThisShift(waiterShifts, shift);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EnsureCanWaiterWork_IfTrueIfShiftToTakeStartsAsAnotherEnds()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto(){StartTime = new DateTime(date.Year,date.Month,date.Day,13,00,00), EndTime = new DateTime(date.Year,date.Month,date.Day,15,00,00)}
            };

            var shift = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 15, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 18, 00, 00) };

            var result = CanWaiterWorkThisShift(waiterShifts, shift);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EnsureCanWaiterWork_IfFalseIfShiftToTakeStartsAsAnotherStarts()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto(){StartTime = new DateTime(date.Year,date.Month,date.Day,13,00,00), EndTime = new DateTime(date.Year,date.Month,date.Day,15,00,00)}
            };

            var endsAfter = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 13, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 18, 00, 00) };
            var endsBefore = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 13, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 14, 00, 00) };

            var resultEndingAfter = CanWaiterWorkThisShift(waiterShifts, endsAfter);
            var resultEndingBefore = CanWaiterWorkThisShift(waiterShifts, endsAfter);

            Assert.IsFalse(resultEndingAfter);
            Assert.IsFalse(resultEndingBefore);
        }

        [TestMethod]
        public void EnsureCanWaiterWork_IfFalseIfShiftToTakeEndsAsAnotherEnds()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto() {StartTime = new DateTime(date.Year, date.Month, date.Day, 14, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 16, 00, 00)}
            };

            var startsBefore = new ShiftDto() {IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 13, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 16, 00, 00)};
            var startsDuring = new ShiftDto() {IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 15, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 16, 00, 00)};

            var resultBefore = CanWaiterWorkThisShift(waiterShifts, startsBefore);
            var resultDuring = CanWaiterWorkThisShift(waiterShifts, startsDuring);

            Assert.IsFalse(resultBefore);
            Assert.IsFalse(resultDuring);
        }

        [TestMethod]
        public void EnsureCanWaiterWork_IfFalseIfShiftToTakeStartsAndEndsAtSameTimeAsAnother()
        {
            var waiterShifts = new List<ShiftDto>()
            {
                new ShiftDto(){StartTime = new DateTime(date.Year,date.Month,date.Day,14,00,00), EndTime = new DateTime(date.Year,date.Month,date.Day,16,00,00)}
            };

            var shift = new ShiftDto() { IsAvailable = true, StartTime = new DateTime(date.Year, date.Month, date.Day, 14, 00, 00), EndTime = new DateTime(date.Year, date.Month, date.Day, 16, 00, 00) };

            var result = CanWaiterWorkThisShift(waiterShifts, shift);

            Assert.IsFalse(result);
        }
    }
}
