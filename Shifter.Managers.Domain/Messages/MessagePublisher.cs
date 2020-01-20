using Framework.PubSub.Messages;
using Framework.PubSub.Msmq;

namespace Shifter.Managers.Domain.Messages
{
    public static class MessagePublisher
    {
        #region Constants

        private static readonly Publisher publisher;

        #endregion

        #region Constructors

        static MessagePublisher()
        {
            publisher = new Publisher(Config.MessageQueueName);
        }

        #endregion

        public static void PublishApplicationMessage(string topic, object messageBody)
        {
            publisher.Publish(MessageFactory.CreateAppMessage(messageBody, topic));
        }

        public static void PublishComsMessage(string messageBody, string subject, string toAddress)
        {
            publisher.Publish(MessageFactory.CreateEmailMessage(messageBody, subject, toAddress));
        }
    }
}
