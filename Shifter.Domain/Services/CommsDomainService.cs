using Framework;
using Framework.Domain;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public class CommsDomainService : ICommsDomainService
    {
        #region Constructors

        public CommsDomainService(IRepository repository)
        {
            Guard.ArgumentNotNull(repository, "repository");

            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        #region Implementation of IStaffService

        public NotificationCollection SaveEmailNotification(string subject, string message, string fromEmailAddress, string toEmailAddress)
        {
            Guard.ArgumentNotNull(message, "message");
            Guard.ArgumentNotNull(fromEmailAddress, "fromEmailAddress");
            Guard.ArgumentNotNull(toEmailAddress, "toEmailAddress");

            var result = repository.Save(EmailNotification.Create(subject, message, fromEmailAddress, toEmailAddress));

            return result;
        }

        #endregion
    }
}
