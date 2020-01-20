using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IManagerDomainService
    {
        Manager LoadManager(string username, string password);

        Manager LoadManager(int managerId);

        NotificationCollection ResetPassword(int managerId);

        Manager FindManager(string username);

        NotificationCollection UpdatePassword(int managerId, string password);

        NotificationCollection SaveManager(Manager manager);
    }
}
