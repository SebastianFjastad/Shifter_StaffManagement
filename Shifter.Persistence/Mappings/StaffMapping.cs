using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class StaffMapping : ClassMap<Staff>
    {
        public StaffMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "StaffId");
            References(c => c.UserAccount).Column("UserAccountId").Cascade.SaveUpdate().Not.LazyLoad();
            References(c => c.StaffType).Column("StaffTypeId").Cascade.None().Not.LazyLoad();
            
            Map(c => c.CanSwapShifts);
            Map(c => c.CanWorkDoubles);
            Map(c => c.MaxNumberOfShiftsPerWeek);
            Map(c => c.IsExperienced);
            Map(c => c.IsActive);
            Map(c => c.DateLastActive);
            Map(c => c.WelcomeEmailSent);

            HasMany(c => c.UnavailabilityRecords).KeyColumn("StaffId").Cascade.None().Not.LazyLoad(); ; ;
            
            HasManyToMany(c => c.Restaurants)
                .Table("StaffRestaurant")
                .ParentKeyColumn("StaffId")
                .ChildKeyColumn("RestaurantId")
                .Not.LazyLoad();

            Table("Staff");
        }
    }
}