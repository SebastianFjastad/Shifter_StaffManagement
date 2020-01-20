using System;
using Framework.PubSub.Msmq;

namespace Framework.PubSub.Messages
{
    public static class MessageFactory
    {
        public static IMessage CreateAppMessage(object messageBody, string topic, string clientKey = "")
        {
            return new ApplicationMessage
            {
                Header = new MessageHeader
                {
                    Topic = topic,
                    TimeStamp = DateTime.Now.Ticks,
                    Sender = clientKey
                },
                Body = messageBody
            };
        }
    }
}