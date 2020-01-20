using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class StaffTypeMapping : ClassMap<StaffType>
    {
        public StaffTypeMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "StaffTypeId");
            
            Map(c => c.Name, "StaffTypeName");

            Table("StaffType");
        }
    }
}