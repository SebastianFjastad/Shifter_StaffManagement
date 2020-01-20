
namespace Framework.Notifications
{
    /// <summary>
    /// Gives an indication of the status or severity of a <see cref="Notification"/>
    /// </summary>
    public enum NotificationSeverity
    {
        /// <summary>
        /// <see cref="Notification"/> contains informational detail.
        /// </summary>
        Information,

        /// <summary>
        /// <see cref="Notification"/> contains a warning.
        /// </summary>
        Warning,

        /// <summary>
        /// <see cref="Notification"/> contains an error.
        /// </summary>
        Error
    }
}
