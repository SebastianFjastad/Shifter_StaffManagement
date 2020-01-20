using FluentNHibernate.Mapping;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Mappings
{
    public class RestaurantMapping : ClassMap<Restaurant>
    {
        public RestaurantMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "RestaurantId");
            Map(c => c.Name);
            HasManyToMany(c => c.Managers).Table("ManagerRestaurant").ParentKeyColumn("RestaurantId").ChildKeyColumn("ManagerId");
            HasMany(c => c.ShiftTemplates).KeyColumn("RestaurantId");
            HasMany(c => c.Settings).KeyColumn("RestaurantId");
            HasManyToMany(c => c.Waiters).Table("WaiterRestaurant").ParentKeyColumn("RestaurantId").ChildKeyColumn("WaiterId");

            Table("Restaurant");
        }
    }
}