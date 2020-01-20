using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class EmailNotification : Entity
    {
        #region Properties

        public virtual string ToEmailAddress { get; set; }

        public virtual string FromEmailAddress { get; set; }

        public virtual string Body { get; set; }

        public virtual string Subject { get; set; }

        public virtual int NumberOfAttempts { get; set; }

        public virtual string Status { get; set; }

        public virtual bool IsSent { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of an email notification
        /// </summary>
        public static EmailNotification Create(string subject, string body, string fromEmailAddress, string toEmailAddress)
        {
            return new EmailNotification
            {
                ToEmailAddress = toEmailAddress,
                FromEmailAddress = fromEmailAddress,
                Body = body,
                Subject = subject
            };
        }

        #endregion
    }
}
