using System;

namespace Framework.PubSub.Messages
{
    [Serializable]
    public class ApplicationMessage : IMessage
    {
        public IMessageHeader Header { get; set; }
        public object Body { get; set; }
    }
}