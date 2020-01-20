using System;
using Framework;
using Framework.Notifications;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class DeleteWaiterAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public DeleteWaiterAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnComplete { get; set; }

        public Func<NotificationCollection, T> OnFailure { get; set; }

        #endregion

        #region Methods

        public T Invoke(int waiterId, int restaurantId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            Guard.InstanceNotNull(OnFailure, "OnFailure");

            var response = serviceRegistry.StaffService.DeleteStaff(new GenericStaffRequest { StaffId = waiterId });

            return response.NotificationCollection.HasErrors()? OnFailure.Invoke(response.NotificationCollection) : OnComplete.Invoke();
        }

        #endregion
    }
}