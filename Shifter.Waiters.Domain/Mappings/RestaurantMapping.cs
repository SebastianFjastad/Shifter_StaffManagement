using FluentNHibernate.Mapping;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain.Mappings
{
    public class RestaurantMapping : ClassMap<Restaurant>
    {
        public RestaurantMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "RestaurantId");
            Map(c => c.Name);
            HasManyToMany(c => c.Waiters).Table("WaiterRestaurant").ParentKeyColumn("RestaurantId").ChildKeyColumn("WaiterId");

            Table("Restaurant");
        }
    }
}