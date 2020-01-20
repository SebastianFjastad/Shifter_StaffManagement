using System;
using NHibernate;

namespace Shifter.Persistence.Data
{
    public interface IDatabaseFactory: IDisposable
    {
        ISession Session();

        string MappingAssembly { get; }
    }
}
