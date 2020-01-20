using System;
using System.Linq;
using DotNetOpenAuth.Messaging;
using Framework;
using Framework.Notifications;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class SaveWaiterAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public SaveWaiterAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnSuccess { get; set; }

        public Func<NotificationCollection, T> OnFailure { get; set; }

        #endregion

        #region Methods

        public T Invoke(StaffMemberViewModel viewModel, int restaurantId)
        {
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");
            Guard.InstanceNotNull(OnFailure, "OnFailure");
            Guard.InstanceNotNull(viewModel, "viewModel");

            var request = new SaveWaiterRequest
            {
                Staff = viewModel.Staff, 
                RestaurantId = restaurantId, 
                StaffMemberHasEmailAddress = !viewModel.StaffMemberHasNoEmailAddress
            };

            var saveWaiterResponse = serviceRegistry.StaffService.SaveStaff(request);

            return saveWaiterResponse.NotificationCollection.HasErrors() ? OnFailure(saveWaiterResponse.NotificationCollection) : OnSuccess();
        }

        #endregion

    }
}