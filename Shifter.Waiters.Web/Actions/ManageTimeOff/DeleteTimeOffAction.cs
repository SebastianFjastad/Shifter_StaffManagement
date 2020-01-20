using System;
using System.Collections.Generic;
using Framework;
using Framework.Notifications;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Waiters.Web.Actions.ManageTimeOff
{
    public class DeleteTimeOffAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public DeleteTimeOffAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnSuccess { get; set; }

        public Func<NotificationCollection,T> OnFailure { get; set; }

        #endregion

        #region Methods

        public T Invoke(int unavailabilityId)
        {
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");
            Guard.InstanceNotNull(OnFailure, "OnFailure");

            var result = serviceRegistry.StaffService.DeleteUnavailability(new DeleteUnavailabilityRequest() {UnavailabilityIds = new List<int> {unavailabilityId}});

            return result.NotificationCollection.HasErrors() ? OnFailure(result.NotificationCollection) : OnSuccess();
        }

        #endregion
    }
}