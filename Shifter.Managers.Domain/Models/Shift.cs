using System;
using Framework.Domain;

namespace Shifter.Managers.Domain.Models
{
    public class Shift : Entity
    {
        #region properties

        public virtual Waiter Waiter { get; set; }
               
        public virtual Restaurant Restaurant { get; set; }
               
        public virtual DateTime Date { get; set; }
               
        public virtual TimeSpan StartTime { get; set; }
               
        public virtual TimeSpan EndTime { get; set; }
               
        public virtual bool IsAvailable { get; set; }
               
        public virtual bool IsCancelled { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the waiters name who this shift is assigned to or "un-assigned" not assigned to a waiter.
        /// </summary>
        public string GetAssignedToHint()
        {
            var lable = "Unassigned";

            if (Waiter.IsNotNull())
            {
                lable = Waiter.FirstName;
            }

            return lable;
        }

        #endregion
    }
}
