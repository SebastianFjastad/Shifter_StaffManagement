using Framework.Domain;
using System;

namespace Shifter.Domain.Model.Entities
{
    public class TrackedDeletedShift : Entity
    {
        #region properties

        public virtual int ShiftId { get; set; }
               
        public virtual int RestaurantId { get; set; }

        public virtual int? StaffId { get; set; }

        public virtual DateTime ShiftStartTime { get; set; }

        public virtual DateTime ShiftEndTime { get; set; }

        public virtual DateTime DateDeleted { get; set; }

        public virtual DateTime? DateStaffConfirmed { get; set; }

        #endregion
    }
}
