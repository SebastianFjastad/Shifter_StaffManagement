using FluentNHibernate.Mapping;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Mappings
{
    public class ManagerMapping : ClassMap<Manager>
    {
        public ManagerMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "ManagerId");
            References(c => c.UserAccount).Column("UserAccountId").Cascade.All().Not.LazyLoad();
            
            Map(c => c.FirstName);
            Map(c => c.LastName);

            Map(c => c.ContactNumber);
            Map(c => c.EmailAddress);
            HasManyToMany(c => c.Restaurants).Table("ManagerRestaurant").ParentKeyColumn("ManagerId").ChildKeyColumn("RestaurantId");

            Table("Manager");
        }
    }
}