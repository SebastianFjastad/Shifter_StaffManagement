using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class SettingsMapping : ClassMap<Settings>
    {
        public SettingsMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "SettingsId");
            Map(c => c.RestaurantId);

            Map(c => c.UnassignedShiftNotificationPeriod, "UnassignedShiftNotificationPeriod");
            Map(c => c.NumDaysBeforeShiftSwappingLockDown, "NumDaysBeforeShiftSwappingLockDown");

            Table("Settings");
        }
    }
}