using Framework;
using Framework.Domain;
using Framework.Email;
using Framework.PubSub;
using Shifter.Domain.Model.Entities;
using System;

namespace Shifter.PublicationsService
{
    public class Processor : IDisposable
    {

        private readonly int threshold = Config.ProcessThreshold;

        private IRepository repository;
        private ISubscriber subscriber;

        public Processor(IRepository repository)
        {
            Guard.ArgumentNotNull(repository, "repository");

            log4net.Config.XmlConfigurator.Configure();

            this.repository = repository;
        }

        public void Dispose()
        {
            if (repository.IsNotNull())
            {
                repository.Dispose();
                repository = null;
            }
        }

        public void Process()
        {
            //Find all messages that have not been sent and have not exceeded the execution threshold
            var messages = repository.FindBy<EmailNotification>(p => p.IsSent == false && p.NumberOfAttempts < threshold);

            var client = EmailUtils.CreateSmtpClient();

            foreach (var message in messages)
            {
                message.IsSent = false;
                message.NumberOfAttempts++;

                try
                {
                    Guard.ArgumentNotEmpty(message.ToEmailAddress, "message.ToEmailAddress");
                    Guard.ArgumentNotEmpty(message.FromEmailAddress, "message.FromEmailAddress");

                    var email = EmailUtils.CreateMailMessage(message.Subject, message.Body, message.FromEmailAddress, message.ToEmailAddress);

                    client.Send(email);

                    message.IsSent = true;
                    message.Status = "Success";
                }
                catch (Exception ex)
                {
                    message.Status = ex.Message;
                }
                finally
                {
                    repository.Save(message);
                }
            }
        }
    }
}
