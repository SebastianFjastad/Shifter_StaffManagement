using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.Extrensions
{
    [TestClass]
    public class StringExtensionsTestFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            var val = "name ~`@$%^&*()_+={[]};:|?/.<,>";

            var result = val.RemoveWhiteSpaceAndSpecialChar();

            Assert.IsTrue(result == "name");
        }
    }
}
