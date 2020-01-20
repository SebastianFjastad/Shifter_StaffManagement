
namespace Framework.Encryption
{
    /// <summary>
    /// Represents a class used to generate a password hash based on PBKDF2 (Password-Based Key Derivation Function 2)  
    /// </summary>
    public class PasswordEncryptor : IPasswordEncryptor
    {
        private readonly ICryptoService cryptoService;

        public PasswordEncryptor(ICryptoService cryptoService)
        {
            Guard.ArgumentNotNull(cryptoService, "cryptoService");

            this.cryptoService = cryptoService;
        }

        /// <summary>
        /// Generates a password hash
        /// </summary>
        public string GenerateHash(string password, string salt)
        {
            return cryptoService.Compute(password, salt);
        }

        /// <summary>
        /// Generates salt
        /// </summary>
        public string GenerateSalt()
        {
            return cryptoService.GenerateSalt();
        }
    }
}