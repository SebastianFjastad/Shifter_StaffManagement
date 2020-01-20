using System;
using Framework;
using Framework.Encryption;
using Shifter.Service.Api.Requests;
using Shifter.Web.ViewModels.Account;

namespace Shifter.Web.Actions.Account
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

        public Func<ResetPasswordResultViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(string userAccountId)
        {
            Guard.ArgumentNotEmpty(userAccountId, "userAccountId");

            var userAccountIdDecrypted = int.Parse(userAccountId.Decrypt());

            var resetPasswordResponse = serviceRegistry.AuthenticationService.ResetPassword(new GenericEntityRequest(userAccountIdDecrypted));

            var model = new ResetPasswordResultViewModel();

            if (resetPasswordResponse.NotificationCollection.HasErrors())
            {
                model.Heading = "Error";
                model.Message = "Sorry we were not able to send you your new password.";
            }
            else
            {
                model.Heading = "Success";
                string.Format("Your new password has been emailed to {0}", resetPasswordResponse.EmailAddress);
            }

            return OnComplete.Invoke(model);
        }

        #endregion
    }
}