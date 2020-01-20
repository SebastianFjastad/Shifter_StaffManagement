using FluentNHibernate.Mapping;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Mappings
{
    public class WaiterMapping : ClassMap<Waiter>
    {
        public WaiterMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "WaiterId");
            References(c => c.UserAccount).Column("UserAccountId").Cascade.SaveUpdate().Not.LazyLoad();
            
            Map(c => c.FirstName);
            Map(c => c.LastName);

            Map(c => c.CanSwapShifts);
            Map(c => c.CanWorkDoubles);
            Map(c => c.MaxNumberOfShiftsPerWeek);
            Map(c => c.IsExperienced);

            Map(c => c.ContactNumber);
            Map(c => c.EmailAddress);
            HasManyToMany(c => c.Restaurants).Table("WaiterRestaurant").ParentKeyColumn("WaiterId").ChildKeyColumn("RestaurantId").Not.LazyLoad();
            HasMany(c => c.Shifts).KeyColumn("WaiterId").Cascade.None().Not.LazyLoad();

            Table("Waiter");
        }
    }
}