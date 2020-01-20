using System;
using Framework;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Services;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Shared.WebClient.Actions
{
    public class SavePersonalDetailsAction<T> where T : class
    {
        #region Locals

        private readonly IAuthenticationService authenticationService;

        #endregion

        #region Constructors

        public SavePersonalDetailsAction(IAuthenticationService authenticationService)
        {
            Guard.ArgumentNotNull(authenticationService, "authenticationService");

            this.authenticationService = authenticationService;
        }

        #endregion

        #region Properties

        public Func<PersonalDetailsViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(PersonalDetailsViewModel viewModel, int userAccountId)
        {
            if (viewModel.NewPassword != viewModel.ConfirmPassword)
            {
                viewModel.Notifications.AddError("Passwords don't match.");
            }
            else
            {
                var pasword = viewModel.NewPassword.SafeTrim();
                if (pasword.IsNotNullOrEmpty())
                {
                    var updatePasswordResponse = authenticationService.SaveNewPassword(new SaveNewPasswordRequest(){Password = pasword, UserAccountId = userAccountId});

                    viewModel.Notifications += updatePasswordResponse.NotificationCollection;
                }

                var request = new UpdateUserAccountRequest()
                {
                    UserAccountId = userAccountId,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    EmailAddress = viewModel.EmailAddress,
                    ContactNumber = viewModel.ContactNumber
                };

                var saveResponse = authenticationService.UpdateUserAccount(request);

                viewModel.Notifications += saveResponse.NotificationCollection;

                if (!viewModel.HasErrors)
                {
                    viewModel.Notifications.AddMessage(new Notification("Save successful.", NotificationSeverity.Information));
                }
            }

            return OnComplete.Invoke(viewModel);
        }

        #endregion

    }
}