using System;
using FakeItEasy;
using Framework.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.Encryption
{
    [TestClass]
    public class DateTimeExtensionsTestFixture
    {
        [TestMethod]
        public void EnsureStartOfWeekReturnsTheMondayIfDateIsSunday()
        {
            var date = new DateTime(2014, 06, 15);
            var startOfWeek = date.StartOfWeek();
            Assert.IsTrue(startOfWeek.DayOfWeek == DayOfWeek.Monday);
            Assert.IsTrue(startOfWeek.Date == new DateTime(2014, 06, 09));
        }

        [TestMethod]
        public void EnsureStartOfWeekReturnsTheMondayIfDateIsMonday()
        {
            var date = new DateTime(2014, 06, 09);
            var startOfWeek = date.StartOfWeek();
            Assert.IsTrue(startOfWeek.DayOfWeek == DayOfWeek.Monday);
            Assert.IsTrue(startOfWeek.Date == new DateTime(2014, 06, 09));
        }
    }
}
