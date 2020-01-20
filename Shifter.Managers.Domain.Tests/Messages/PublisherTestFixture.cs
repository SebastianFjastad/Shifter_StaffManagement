using System;
using System.Threading.Tasks;
using Framework.PubSub.Messages;
using Framework.PubSub.Msmq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Managers.Domain.Messages;

namespace Shifter.Managers.Domain.Tests.Messages
{
    [TestClass]
    public class PublisherTestFixture
    {
        private static bool executed = false;

        [TestMethod]
        public void TestMethod1()
        {
            var subscribeTask = new Task(ProcessMessagesFromQueue);

            subscribeTask.Start();

            MessagePublisher.PublishApplicationMessage("test topic", "message");
        }

        private static void ProcessMessagesFromQueue()
        {
            var subscriber = new Subscriber(
                message =>
                {
                    //if message is app message then message processed - notify web app 
                    if (message is ApplicationMessage)
                    {
                        var appMessage = message as ApplicationMessage;
                        executed = true;
                    }
                },
                exception =>
                {
                    //log exception
                });

            subscriber.Subscribe(Config.MessageQueueName);
        }
    }
}
