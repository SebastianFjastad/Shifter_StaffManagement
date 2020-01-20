using Framework.Domain;
using Framework.Notifications;

namespace Framework.Rules
{
    public interface IRule
    {
        NotificationCollection Validate(object entity, IRepository repository);
    }
}