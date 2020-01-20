using System;

namespace Framework.PubSub.Messages
{
    public interface IMessageHeader
    {
        long TimeStamp { get; set; }

        string Topic { get; set; }

        int NumberOfAttempts { get; set; }

        string Sender { get; set; }

        string Receiver { get; set; }

        Guid CorrelationId { get; set; }
    }
}