using FluentNHibernate.Mapping;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Mappings
{
    public class ShiftTimeslotMapping : ClassMap<ShiftTimeslot>
    {
        public ShiftTimeslotMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "ShiftTimeslotId");
            Map(c => c.RestaurantId);

            Map(c => c.StartTime).CustomType("TimeAsTimeSpan");
            Map(c => c.EndTime).CustomType("TimeAsTimeSpan");

            Table("ShiftTimeslot");
        }
    }
}