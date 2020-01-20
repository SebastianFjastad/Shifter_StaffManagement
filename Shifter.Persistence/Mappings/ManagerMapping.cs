using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class ManagerMapping : ClassMap<Manager>
    {
        public ManagerMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "ManagerId");
            References(c => c.UserAccount).Column("UserAccountId").Cascade.All().Not.LazyLoad();
            
            HasManyToMany(c => c.Restaurants)
                .Table("ManagerRestaurant")
                .ParentKeyColumn("ManagerId")
                .ChildKeyColumn("RestaurantId")
                .Not.LazyLoad();

            Table("Manager");
        }
    }
}