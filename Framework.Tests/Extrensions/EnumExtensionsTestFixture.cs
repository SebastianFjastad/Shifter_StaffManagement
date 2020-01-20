using System;
using Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.Encryption
{
    [TestClass]
    public class EnumExtensionsTestFixture
    {
        [TestMethod]
        public void EnsureDescriptionIsReturnedIfAvailable()
        {
            var testEnum = TestEnum.One;

            var description = testEnum.Description();

            Assert.IsTrue(description == "First");
        }

        [TestMethod]
        public void EnsureDescriptionIsValueIfNoDescriptionAvailable()
        {
            var testEnum = TestEnum.Two;

            var description = testEnum.Description();

            Assert.IsTrue(description == "Two");
        }
    }

    enum TestEnum
    {
        [System.ComponentModel.Description("First")]
        One,
        Two
    }
}
