using FluentNHibernate.Mapping;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Mappings
{
    public class ShiftMapping : ClassMap<Shift>
    {
        public ShiftMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "ShiftId");
            Map(c => c.Date);
            Map(c => c.EndTime).CustomType("TimeAsTimeSpan");
            Map(c => c.StartTime).CustomType("TimeAsTimeSpan");
            Map(c => c.IsAvailable);
            Map(c => c.IsCancelled);
            References(c => c.Waiter).Column("WaiterId");
            References(c => c.Restaurant).Column("RestaurantId");

            Table("Shift");
        }
    }
}