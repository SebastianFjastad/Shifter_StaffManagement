using System;
using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class ShiftTimeslot : Entity
    {
        public virtual int RestaurantId { get; set; }

        public virtual int StaffTypeId { get; set; }

        public virtual TimeSpan StartTime { get; set; }

        public virtual TimeSpan EndTime { get; set; }
    }
}
