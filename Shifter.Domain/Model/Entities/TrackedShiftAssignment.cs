using Framework.Domain;
using System;

namespace Shifter.Domain.Model.Entities
{
    public class TrackedShiftAssignment : Entity
    {
        #region properties

        public virtual int ShiftId { get; set; }
               
        public virtual int RestaurantId { get; set; }
               
        public virtual DateTime DateAssigned { get; set; }

        public virtual DateTime? DateRecieved { get; set; }

        public virtual int StaffId { get; set; }

        public virtual bool IsStillValid { get; set; }

        #endregion
    }
}
