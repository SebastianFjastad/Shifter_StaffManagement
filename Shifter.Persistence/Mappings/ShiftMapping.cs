using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class ShiftMapping : ClassMap<Shift>
    {
        public ShiftMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "ShiftId");
            Map(c => c.EndTime);
            Map(c => c.StartTime);
            Map(c => c.IsAvailable);
            Map(c => c.IsCancelled);
            References(c => c.Staff).Column("StaffId");
            References(c => c.Restaurant).Column("RestaurantId");

            Table("Shift");
        }
    }
}