using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class ShiftTimeslotMapping : ClassMap<ShiftTimeslot>
    {
        public ShiftTimeslotMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "ShiftTimeslotId");
            Map(c => c.RestaurantId);
            Map(c => c.StaffTypeId);

            Map(c => c.StartTime).CustomType("TimeAsTimeSpan");
            Map(c => c.EndTime).CustomType("TimeAsTimeSpan");

            Table("ShiftTimeslot");
        }
    }
}