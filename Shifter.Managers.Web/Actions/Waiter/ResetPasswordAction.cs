using Framework;
using System;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class ResetPasswordAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public ResetPasswordAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnSuccess { get; set; }

        public Func<T> OnFailure { get; set; }

        #endregion

        #region Methods

        public T Invoke(int waiterId)
        {
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");
            Guard.InstanceNotNull(OnFailure, "OnFailure");

            var result = serviceRegistry.StaffService.ResetPassword(new GenericStaffRequest {StaffId = waiterId});

            return result.NotificationCollection.HasErrors() ? OnFailure() : OnSuccess();
        }

        #endregion

    }
}