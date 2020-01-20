using Framework;
using Framework.PubSub.Messages;
using Framework.PubSub.Msmq;
using System.Threading.Tasks;

namespace Shifter.Waiters.Web.Hubs
{
    public static class MessageProcessor
    {

        #region Constructor

        public static void Initialize()
        {
            var subscribeTask = new Task(ProcessMessagesFromQueue);

            subscribeTask.Start();
        }

        #endregion

        private static void ProcessMessagesFromQueue()
        {
            var subscriber = new Subscriber(
                message =>
                {
                    //if message is app message then message processed - notify web app 
                    if (message is ApplicationMessage)
                    {
                        var appMessage = message as ApplicationMessage;
                        MessageBroadcaster.Instance.RaiseEvent(appMessage.Header.Topic, appMessage.Body, message.Header.Sender);
                    }
                },
                exception =>
                {
                    //log exception
                });

            subscriber.Subscribe(SharedConfig.MessageQueueName);
        }
    }
}
