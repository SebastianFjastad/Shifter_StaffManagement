
using System;
using Framework.Domain;

namespace Shifter.Managers.Domain.Models
{
    public class Settings : Entity
    {
        public virtual int RestaurantId { get; set; }

        public virtual int UnassignedShiftNotificationPeriod { get; set; }
    }
}
