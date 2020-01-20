using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Notifications;
using Framework.Rules;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Waiters.Web.Actions.ManageTimeOff
{
    public class SaveTimeOffAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public SaveTimeOffAction(IServiceRegistry serviceRegistry)
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

        public T Invoke(int staffMemberId, StaffUnavailabilityRecordDto unavailability)
        {
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");
            Guard.InstanceNotNull(OnFailure, "OnFailure");

            var result = serviceRegistry.StaffService.SaveUnavailability(new SaveUnavailabilityRequest() { StaffId = staffMemberId, Unavailability = new List<StaffUnavailabilityRecordDto> { unavailability } });

            var staffSpecificErrors = result.NotificationCollection.Errors(ClientErrorCodes.Staff);

            return staffSpecificErrors.Any() ? OnFailure(staffSpecificErrors) : OnSuccess();
        }

        #endregion
    }
}