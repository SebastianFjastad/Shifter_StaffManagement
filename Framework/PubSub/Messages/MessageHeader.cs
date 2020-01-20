using System;

namespace Framework.PubSub.Messages
{
    [Serializable]
    public class MessageHeader : IMessageHeader
    {
        public long TimeStamp { get; set; }
        public string Topic { get; set; }
        public int NumberOfAttempts { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public Guid CorrelationId { get; set; }
    }
}