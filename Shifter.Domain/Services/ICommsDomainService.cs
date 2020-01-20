using Framework.Notifications;

namespace Shifter.Domain.Services
{
    public interface ICommsDomainService
    {
        NotificationCollection SaveEmailNotification(string subject, string message, string fromEmailAddress, string toEmailAddress);
    }
}
