using NHibernate;

namespace Shifter.Persistence.Data
{
    public interface IDatabaseFactory
    {
        ISession Session();

        string MappingAssembly { get; }
    }
}
