using Framework;
using Framework.Notifications;
using Shifter.Service.Api.Requests;
using System;
using System.Collections.Generic;

namespace Shifter.Waiters.Web.Actions
{
    public class ConfirmNotificationsReceived<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public ConfirmNotificationsReceived(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<NotificationCollection,T> OnFailure { get; set; }

        public Func<T> OnSuccess { get; set; }

        #endregion

        #region Methods

        public T Invoke(Dictionary<int, string> notifications)
        {
            var notificationsResult = serviceRegistry.StaffService.ConfirmStaffReceivedNotifications(new ConfirmNotificationReceivedRequest(notifications));

            return notificationsResult.NotificationCollection.HasErrors() ? OnFailure(notificationsResult.NotificationCollection) : OnSuccess();
        }

        #endregion

    }
}