using System.Web.UI.WebControls;
using Framework;
using Framework.Encryption;
using Framework.Notifications.Extensions;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Waiters.Web.ViewModels;
using System;

namespace Shifter.Waiters.Web.Actions
{
    public class LoadWaiterNotificationsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadWaiterNotificationsAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<NotificationsViewModel,T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, int waiterId)
        {
            var notificationsResult = serviceRegistry.StaffService.LoadStaffNotifications(new LoadByStaffAndRestaurantRequest(waiterId, restaurantId));
            var model = new NotificationsViewModel()
            {
                Notifications = notificationsResult.NotificationCollection
            };

            return OnComplete(model);
        }

        #endregion

    }
}