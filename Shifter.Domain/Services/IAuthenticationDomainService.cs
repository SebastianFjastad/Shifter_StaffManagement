using Framework.Notifications;
using Shifter.Domain.Model;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IAuthenticationDomainService
    {
        UserAccount FindUserAccount(string username, string password);

        UserAccount FindUserAccount(string username);

        NotificationCollection SaveUserAccount(UserAccount userAccount);

        T LoadProfileByUserAccount<T>(int userAccountId) where T : IProfile;

        UserAccount LoadUserAccount(int userAccountId);

        NotificationCollection ResetPassword(int userAccountId);

        NotificationCollection SaveNewPassword(int userAccountId, string password);
    }
}
