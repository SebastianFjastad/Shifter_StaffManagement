
namespace Framework.Encryption
{
    /// <summary>
    /// Interface for Simple Crypto Service
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Gets or sets the number of iterations the hash will go through
        /// </summary>
        int HashIterations { get; set; }

        /// <summary>
        /// Gets or sets the size of salt that will be generated if no Salt was set
        /// </summary>
        int SaltSize { get; set; }

        /// <summary>
        /// Gets or sets the plain text to be hashed
        /// </summary>
        string PlainText { get; set; }

        /// <summary>
        /// Gets the base 64 encoded string of the hashed PlainText
        /// </summary>
        string HashedText { get; }

        /// <summary>
        /// Gets or sets the salt that will be used in computing the HashedText. This contains both Salt and HashIterations.
        /// </summary>
        string Salt { get; set; }

        /// <summary>
        /// Compute the hash that will utilize the passed salt
        /// </summary>
        /// <param name="textToHash">The text to be hashed</param>
        /// <param name="salt">The salt to be used in the computation</param>
        /// <returns>the computed hash: HashedText</returns>
        string Compute(string textToHash, string salt);

        /// <summary>
        /// Generates a salt with default salt size
        /// </summary>
        /// <returns>the generated salt</returns>
        string GenerateSalt();
    }
}