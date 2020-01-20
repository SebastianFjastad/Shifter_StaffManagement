using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Notifications;
using Shifter.Managers.Web.Utils;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class CheckForMessagesAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public CheckForMessagesAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<int, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var result = serviceRegistry.SettingsService.LoadRestaurantNotifications(new GenericEntityRequest(restaurantId));

            var numberOfMessages = result.NotificationCollection.Count(n => n.Severity == NotificationSeverity.Information);

            return OnComplete.Invoke(numberOfMessages);
        }

        #endregion
    }
}