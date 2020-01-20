using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class TrackedDeletedShiftMapping : ClassMap<TrackedDeletedShift>
    {
        public TrackedDeletedShiftMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "TrackingId");
            Map(c => c.RestaurantId);
            Map(c => c.StaffId);
            Map(c => c.ShiftId);
            Map(c => c.ShiftStartTime);
            Map(c => c.ShiftEndTime);
            Map(c => c.DateDeleted);
            Map(c => c.DateStaffConfirmed);

            Table("TrackedDeletedShift");
        }
    }
}