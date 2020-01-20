using System.Collections.Generic;
using System.Linq;

namespace Framework.Notifications.Extensions
{
    /// <summary>
    /// Notification Extension Methods
    /// </summary>
    public static class NotificationExtensions
    {
        /// <summary>
        /// Indicates whether the notification has errors
        /// </summary>
        public static bool HasErrors(this IEnumerable<Notification> notifications)
        {
            return notifications.Contains(m => m.Severity == NotificationSeverity.Error);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has <see cref="Notification"/>.
        /// </summary>
        public static bool HasMessages(this IEnumerable<Notification> notifications)
        {
            return notifications.Any();
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains <see cref="Notification"/> with a severity of Warning.
        /// </summary>
        public static bool HasWarnings(this IEnumerable<Notification> notifications)
        {
            return notifications.Contains(m => m.Severity == NotificationSeverity.Warning);
        }
    }
}
