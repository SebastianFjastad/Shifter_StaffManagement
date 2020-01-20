using Framework.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.Encryption
{
    [TestClass]
    public class StringEncryptorTestFixture
    {
        [TestMethod]
        public void EnsureStringCanBeEncryptedAndDecrypted()
        {
            var plainText = "hello";

            var encrypted = plainText.Encrypt();

            Assert.IsTrue(encrypted != plainText);

            var decrypted = encrypted.Decrypt();

            Assert.IsTrue(decrypted == plainText);
        }
    }
}
