using System;
using System.Messaging;
using Framework.PubSub.Messages;

namespace Framework.PubSub.Msmq
{
    public class Subscriber : ISubscriber
    {
        private readonly Action<IMessage> processDelegate;
        private readonly Action<Exception> processFailed;
        
        private MessageQueue inQueue;
        private IAsyncResult beginPeekAsyncResult;

        public Subscriber(Action<IMessage> processDelegate, Action<Exception> processFailed = null)
        {
            Guard.ArgumentNotNull(processDelegate, "processDelegate");

            this.processDelegate = processDelegate;
            this.processFailed = processFailed;
        }

        private void OnPeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            try
            {
                Process(e.Message.Body);

                inQueue.EndPeek(beginPeekAsyncResult);
                // only receive once peek is processed successfully otherwise the message is lost.
                inQueue.Receive();
                beginPeekAsyncResult = inQueue.BeginPeek();
            }
            catch (Exception ex)
            {
                // Ideally you'd be placing these in a Dead Letter Queue (DLQ) or Poison Queue...
                if (processFailed != null)
                {
                    processFailed(ex);
                }
            }
        }

        public void Process(object message)
        {
            if (!(message is IMessage)) return;

            processDelegate(message as IMessage);
        }

        public void Subscribe(string token)
        {
            inQueue = new MessageQueue(token, QueueAccessMode.ReceiveAndAdmin)
            {
                Formatter = new BinaryMessageFormatter(),
            };
            inQueue.PeekCompleted += OnPeekCompleted;

            beginPeekAsyncResult = inQueue.BeginPeek();
        }

        public void UnSubscribe(string token)
        {
            inQueue.PeekCompleted -= OnPeekCompleted;
            inQueue.Close();
            inQueue = null;
        }
    }
}