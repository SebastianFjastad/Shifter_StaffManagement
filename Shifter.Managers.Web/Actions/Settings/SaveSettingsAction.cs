using Framework;
using Shifter.Managers.Web.ViewModels;
using System;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class SaveSettingsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public SaveSettingsAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<SettingsViewModel, T> OnFailure { get; set; }

        public Func<T> OnSuccess { get; set; }

        #endregion

        #region Methods

        public T Invoke(SettingsDto settings)
        {
            Guard.InstanceNotNull(OnFailure, "OnFailure");
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");

            var viewModel = new SettingsViewModel { Settings = settings };

            var response = serviceRegistry.SettingsService.SaveSettings(new SaveSettingsRequest
            {
                Settings = settings
            });

            viewModel.Notifications = response.NotificationCollection;

            return viewModel.HasErrors ? OnFailure(viewModel) : OnSuccess();

        }

        #endregion
    }
}