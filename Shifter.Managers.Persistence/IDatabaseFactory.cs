using NHibernate;

namespace Shifter.Manager.Persistence
{
    public interface IDatabaseFactory
    {
        ISession Session();
    }
}
