using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface ISettingsDomainService
    {
        Settings LoadSettings(int restaurantId);

        NotificationCollection SaveSettings(Settings settings);
    }
}
