using System;
using Framework;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using log4net.Repository.Hierarchy;
using Shifter.Domain.Services;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Services
{
    public class CommsService : ShifterServiceBase, ICommsService
    {
        private readonly IAuthenticationDomainService authenticationDomainService;
        private readonly ICommsDomainService commsDomainService;

        public CommsService(ICommsDomainService commsDomainService, IAuthenticationDomainService authenticationDomainService)
        {
            Guard.ArgumentNotNull(commsDomainService, "commsDomainService");
            Guard.ArgumentNotNull(authenticationDomainService, "authenticationDomainService");

            this.commsDomainService = commsDomainService;
            this.authenticationDomainService = authenticationDomainService;
        }

        public GenericServiceResponse SaveFeedback(SendFeedbackRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new GenericServiceResponse();

                var userAccount = authenticationDomainService.LoadUserAccount(request.UserAccountId);

                var comsResult = commsDomainService.SaveEmailNotification(request.Title, request.Message, userAccount.EmailAddress, SharedConfig.FeedbackToEmailAddress);

                if (comsResult.HasErrors())
                {
                    base.Logger.Error(comsResult.Errors());

                    result.NotificationCollection.AddError("Sorry we could not send your feedback.");
                }
                else
                {
                    result.NotificationCollection.AddMessage(new Notification("Thanks for the feedback!", NotificationSeverity.Information));
                }

                return result;
            });
        }
    }
}