using Framework.Notifications;

namespace Framework.Domain.Rules.Rules
{
    public interface IRule
    {
        NotificationCollection Validate(object entity);
    }
}