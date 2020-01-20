using System;
using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class ShiftTemplate : Entity
    {
        public ShiftTemplate()
        {
            Timeslot = new ShiftTimeslot();
        }

        public virtual DayOfWeek DayOfWeek { get; set; }

        public virtual ShiftTimeslot Timeslot { get; set; }

        public virtual int RestaurantId { get; set; }

        public virtual int NumberOfWaitersNeeded { get; set; }
    }
}
