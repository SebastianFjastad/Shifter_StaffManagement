using System.Collections.Generic;
using Framework.PubSub.Messages;

namespace Framework.PubSub
{
    public interface IPublisher
    {
        void Publish(IMessage message);

        void Publish(IEnumerable<IMessage> messages);
    }
}
