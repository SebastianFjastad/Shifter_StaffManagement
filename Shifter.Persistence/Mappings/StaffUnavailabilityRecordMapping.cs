using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class StaffUnavailabilityRecordMapping : ClassMap<StaffUnavailabilityRecord>
    {
        public StaffUnavailabilityRecordMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "StaffUnavailabilityId");
            
            Map(c => c.UnavailableFrom);
            Map(c => c.UnavailableTo);
            Map(c => c.StaffId);

            Table("StaffUnavailability");
        }
    }
}