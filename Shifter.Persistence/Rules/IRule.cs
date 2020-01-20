using Framework.Domain;
using Framework.Notifications;

namespace Shifter.Persistence.Rules
{
    public interface IRule
    {
        NotificationCollection Validate(object entity, IRepository repository);
    }
}