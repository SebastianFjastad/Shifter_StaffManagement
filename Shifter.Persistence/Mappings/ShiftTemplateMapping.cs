using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class ShiftTemplateMapping : ClassMap<ShiftTemplate>
    {
        public ShiftTemplateMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "ShiftTemplateId");
            Map(c => c.DayOfWeek);
            Map(c => c.NumberOfWaitersNeeded);
            References(c => c.Timeslot).Column("ShiftTimeslotId").Cascade.None();
            Map(c => c.RestaurantId).Column("RestaurantId");

            Table("ShiftTemplate");
        }
    }
}