using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Framework;
using Framework.PubSub.Messages;
using Framework.PubSub.Msmq;
using Shifter.Managers.Web.Hubs;

namespace Shifter.Managers.Domain.Messages
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
                        MessageBroadcaster.Instance.RaiseEvent(appMessage.Header.Topic, appMessage.Body);
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
