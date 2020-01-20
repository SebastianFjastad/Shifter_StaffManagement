using FluentNHibernate.Mapping;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain.Mappings
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
            Map(c => c.RestaurantId);
            References(c => c.Waiter).Column("WaiterId");

            Table("Shift");
        }
    }
}