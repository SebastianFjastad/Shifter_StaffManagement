namespace Framework.Encryption
{
    /// <summary>
    /// Defines the Contractual Obligations for a Password Encryptor.
    /// </summary>
    public interface IPasswordEncryptor
    {
        /// <summary>
        /// Generates a password hash
        /// </summary>
        string GenerateHash(string password, string salt);

        /// <summary>
        /// Generates salt
        /// </summary>
        string GenerateSalt();
    }
}