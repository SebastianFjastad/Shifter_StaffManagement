using FluentNHibernate.Mapping;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Mappings
{
    public class SettingsMapping : ClassMap<Settings>
    {
        public SettingsMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "SettingsId");
            Map(c => c.RestaurantId);

            Map(c => c.UnassignedShiftNotificationPeriod, "UnassignedShiftNotificationPeriod");

            Table("Settings");
        }
    }
}