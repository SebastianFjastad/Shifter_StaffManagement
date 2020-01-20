using Framework.Notifications;

namespace Framework.Services
{
    public interface IServiceResponse
    {
        NotificationCollection NotificationCollection { get; set; }
    }
}