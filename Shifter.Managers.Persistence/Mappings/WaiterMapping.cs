using FluentNHibernate.Mapping;
using Shifter.Manager.Domain.Models;

namespace Shifter.Manager.Persistence.Mappings
{
    public class WaiterMapping : ClassMap<Waiter>
    {
        public WaiterMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id).GeneratedBy.HiLo("1000");
            
            Map(c => c.FirstName).Not.Nullable();
            Map(c => c.LastName).Not.Nullable();
        }
    }
}