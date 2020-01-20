using Framework;
using Framework.Notifications;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;
using System;
using System.Linq;

namespace Shifter.Managers.Web.Actions
{
    public class LoadSettingsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadSettingsAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<SettingsViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var viewModel = new SettingsViewModel();
            viewModel.Settings.RestaurantId = restaurantId;

            var response = serviceRegistry.SettingsService.LoadSettingsByRestaurant(new LoadSettingsByRestaurantRequest
            {
                RestaurantId = restaurantId
            });

            if (response.IsNotNull() && response.Settings.IsNotNull())
            {
                viewModel.Settings = response.Settings;
            }

            viewModel.Messages = LoadNotifications(restaurantId);

            var staffTypeResult = serviceRegistry.StaffService.LoadStaffTypes();

            viewModel.StaffTypes = staffTypeResult.StaffTypes;

            return OnComplete.Invoke(viewModel);
        }

        private NotificationCollection LoadNotifications(int restaurantId)
        {
            var result = serviceRegistry.SettingsService.LoadRestaurantNotifications(new GenericEntityRequest(restaurantId));
            var notifications = NotificationCollection.CreateEmpty();

            var messages = result.NotificationCollection.Where(n => n.Severity == NotificationSeverity.Information);
            foreach (var notification in messages)
            {
                notifications.AddMessage(notification);
            }
            return notifications;
        }

        #endregion
    }
}