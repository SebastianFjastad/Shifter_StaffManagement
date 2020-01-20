using Framework.Notifications;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Services
{
    public interface ISettingsService
    {
        Settings LoadSettings(int restaurantId);

        NotificationCollection SaveSettings(Settings settings);
    }
}
