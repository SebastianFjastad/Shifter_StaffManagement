using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class Settings : Entity
    {
        public virtual int RestaurantId { get; set; }

        /// <summary>
        /// This is the number of days before a shift is due that a manager would like to be notified if the shift is not assigned
        /// eg. 4 days before a shifts due date and still no one has taken it then notify the manager.
        /// </summary>
        public virtual int UnassignedShiftNotificationPeriod { get; set; }
        
        /// <summary>
        /// The set number of days before a shift is due that it becomes locked down for swapping
        /// eg. 2 days before the shifts due dates manager prohibits swapping
        /// </summary>
        public virtual int NumDaysBeforeShiftSwappingLockDown { get; set; }

    }
}
