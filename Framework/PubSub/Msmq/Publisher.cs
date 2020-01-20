using System;
using System.Collections.Generic;
using System.Messaging;
using Framework.PubSub.Messages;

namespace Framework.PubSub.Msmq
{
    public class Publisher : IPublisher
    {
        private readonly MessageQueue outQueue;

        public Publisher(string destination)
        {
            Guard.ArgumentNotNull(destination, "destination");

            outQueue = new MessageQueue(destination, QueueAccessMode.Send)
            {
                Formatter = new BinaryMessageFormatter()
            };
        }

        public void Publish(IMessage message)
        {
            Guard.ArgumentNotNull(message, "message");

            outQueue.Send(message);
        }

        public void Publish(IEnumerable<IMessage> messages)
        {
            Guard.ArgumentNotNull(messages, "message");

            foreach (var message in messages)
            {
                outQueue.Send(message);
            }
        }
    }
}
