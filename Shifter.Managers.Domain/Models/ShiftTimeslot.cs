
using System;
using Framework.Domain;

namespace Shifter.Managers.Domain.Models
{
    public class ShiftTimeslot : Entity
    {
        public virtual int RestaurantId { get; set; }

        public virtual TimeSpan StartTime { get; set; }

        public virtual TimeSpan EndTime { get; set; }
    }
}
