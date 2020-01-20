using System.ServiceModel;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface IAuthenticationService
    {
        /// <summary>
        /// Attempts to register a new business
        /// </summary>
        [OperationContract]
        GenericServiceResponse Register(RegistrationRequest request);

        /// <summary>
        /// Attempts to authenticate a user with the provided credentials
        /// </summary>
        [OperationContract]
        AuthenticationResponse Authenticate(AuthenticationRequest request);

        /// <summary>
        /// Attempts to update the existing user account
        /// </summary>
        [OperationContract]
        GenericServiceResponse UpdateUserAccount(UpdateUserAccountRequest request);

        /// <summary>
        /// Finds a user account by username
        /// </summary>
        [OperationContract]
        FindUserAccountResponse FindUser(FindUserAccountRequest request);
        
        /// <summary>
        /// Auto generates a new password for the user account and sends it to the users email address
        /// </summary>
        [OperationContract]
        PasswordResetResponse ResetPassword(GenericEntityRequest request);

        /// <summary>
        /// Updates the user accounts password with the provided one.
        /// </summary>
        [OperationContract]
        GenericServiceResponse SaveNewPassword(SaveNewPasswordRequest request);
    }
}
