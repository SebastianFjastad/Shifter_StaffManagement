using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
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
            HasManyToMany(c => c.Staff).Table("StaffRestaurant").ParentKeyColumn("RestaurantId").ChildKeyColumn("StaffId");

            Table("Restaurant");
        }
    }
}