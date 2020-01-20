namespace Framework.Encryption
{
    /// <summary>
    /// Defines the Contractual Obligations for a PasswordGenerator.
    /// </summary>
    public interface IPasswordGenerator
    {
        /// <summary>
        /// Defines the method signature for generating a new password.
        /// </summary>
        /// <returns></returns>
        string NewPassword();
    }
}