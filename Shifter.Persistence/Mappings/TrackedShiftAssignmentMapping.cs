using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class TrackedShiftAssignmentMapping : ClassMap<TrackedShiftAssignment>
    {
        public TrackedShiftAssignmentMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "TrackingId");
            Map(c => c.RestaurantId);
            Map(c => c.DateAssigned);
            Map(c => c.DateRecieved);
            Map(c => c.StaffId);
            Map(c => c.ShiftId);
            Map(c => c.IsStillValid);

            Table("TrackedShiftAssignment");
        }
    }
}