using System.Collections.Generic;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Helpers
{
    /// <summary>
    /// Locates relevant service that can perform persistence operations on a give IMoveAttribute
    /// </summary>
    public static class PersistenceHelper
    {
        /// <summary>
        /// Locates relevant service that can perform persistence operations on the given IMoveAttribute and performs Save operation
        /// </summary>
        public static NotificationCollection Save(Entity entity, IShifterSystem system)
        {
            var notifications = NotificationCollection.CreateEmpty();

            var matchedType = false;

            if (entity is Waiter)
            {
                notifications += system.WaiterService.SaveWaiter(entity as Waiter);
                matchedType = true;
            }

            if (entity is Shift)
            {
                notifications += system.ShiftService.SaveShift(entity as Shift);
                matchedType = true;
            }

            if (entity is ShiftTimeslot)
            {
                notifications += system.ShiftTimeSlotService.SaveTimeslot(entity as ShiftTimeslot);
                matchedType = true;
            }

            if (!matchedType)
            {
                notifications.AddError("ST002", "Type not supported");
            }

            return notifications;
        }

        /// <summary>
        /// Locates relevant service that can perform persistence operations on the given IMoveAttribute and performs Save operation
        /// </summary>
        public static NotificationCollection Save(IEnumerable<Entity> entities, IShifterSystem system)
        {
            var notifications = NotificationCollection.CreateEmpty();

            var matchedType = false;

            if (entities is IEnumerable<ShiftTemplate>)
            {
                notifications += system.ShiftTemplateService.SaveTemplates(entities as IEnumerable<ShiftTemplate>);
                matchedType = true;
            }

            if (!matchedType)
            {
                notifications.AddError("ST001", "Type not supported");
            }

            return notifications;
        }
    }
}