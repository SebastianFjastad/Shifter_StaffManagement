using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class EmailNotificationMapping : ClassMap<EmailNotification>
    {
        public EmailNotificationMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "NotificationId");
            
            Map(c => c.FromEmailAddress);
            Map(c => c.ToEmailAddress);
            Map(c => c.Body);
            Map(c => c.Subject);
            Map(c => c.NumberOfAttempts);
            Map(c => c.Status);
            Map(c => c.IsSent);

            Table("EmailNotification");
        }
    }
}