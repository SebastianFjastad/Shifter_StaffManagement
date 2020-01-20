using System;

namespace Framework.Notifications.Extensions
{
    /// <summary>
    /// Extends a NotificationCollection instance with convenience methods
    /// </summary>
    public static class NotificationCollectionExtensions
    {
        /// <summary>
        /// Provides the ability to add a  <see cref="System.Exception"/> as a Notification message
        /// </summary>
        public static NotificationCollection AddException(this NotificationCollection instance, Exception exception)
        {
            return instance.AddError(exception.Message);
        }


        /// <summary>
        /// Provides the ability to add an error as a Notification message.
        /// </summary>
        public static NotificationCollection AddError(this NotificationCollection instance, string error)
        {
            return instance.AddError(error, string.Empty);
        }

        /// <summary>
        /// Provides the ability to add an error as a Notification message with an optional error code.
        /// </summary>
        public static NotificationCollection AddError(this NotificationCollection instance, string error, string errorCode)
        {
            var notification = new Notification(error, NotificationSeverity.Error);

            if (errorCode.IsNotNullOrEmpty())
            {
                notification.Code = errorCode;
            }

            instance.AddMessage(notification);

            return instance;
        }
    }
}