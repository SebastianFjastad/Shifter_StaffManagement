using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Encryption
{
    /// <summary>
    /// Represents the Password-Based Key Derivation Function 2 implementation
    /// </summary>
    public class PBKDF2 : ICryptoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PBKDF2"/> class.
        /// </summary>
        public PBKDF2()
        {
            //Set default salt size and hashiterations
            HashIterations = 8000;
            SaltSize = 34;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the number of iterations the hash will go through
        /// </summary>
        public int HashIterations
        { get; set; }

        /// <summary>
        /// Gets or sets the size of salt that will be generated if no Salt was set
        /// </summary>
        public int SaltSize
        { get; set; }

        /// <summary>
        /// Gets or sets the plain text to be hashed
        /// </summary>
        public string PlainText
        { get; set; }

        /// <summary>
        /// Gets the base 64 encoded string of the hashed PlainText
        /// </summary>
        public string HashedText
        { get; private set; }

        /// <summary>
        /// Gets or sets the salt that will be used in computing the HashedText. This contains both Salt and HashIterations.
        /// </summary>
        public string Salt
        { get; set; }

        #endregion

        #region Compute

        /// <summary>
        /// Compute the hash that will utilize the passed salt
        /// </summary>
        /// <param name="textToHash">The text to be hashed</param>
        /// <param name="salt">The salt to be used in the computation</param>
        /// <returns>
        /// the computed hash: HashedText
        /// </returns>
        public string Compute(string textToHash, string salt)
        {
            PlainText = textToHash;
            Salt = salt;

            //compute the hash
            HashedText = CalculateHash(HashIterations);

            return HashedText;
        }

        #endregion

        #region Generate Salt

        /// <summary>
        /// Generates a salt with default salt size
        /// </summary>
        public string GenerateSalt()
        {
            if (SaltSize < 1)
            {
                throw new InvalidOperationException(string.Format("Cannot generate a salt of size {0}, use a value greater than 1, recommended: 16", SaltSize));
            }

            var rand = RandomNumberGenerator.Create();

            var ret = new byte[SaltSize];

            rand.GetBytes(ret);

            Salt = Convert.ToBase64String(ret);

            return Salt;
        }

        #endregion

        #region Private Methods

        private string CalculateHash(int iteration)
        {
            //convert the salt into a byte array
            byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(PlainText, saltBytes, iteration))
            {
                var key = pbkdf2.GetBytes(64);
                return Convert.ToBase64String(key);
            }
        }

        #endregion
    }
}