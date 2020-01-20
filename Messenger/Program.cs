using System;
using Framework.PubSub.Messages;
using Framework.PubSub.Msmq;

namespace Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            var publisher = new Publisher(".\\private$\\pubsub.test");

            Console.WriteLine("Publisher Started...");
            Console.WriteLine("Insert shiftid and press <enter> to add to queue. Type exit to quit.");

            var input = Console.ReadLine();
            while(input != "exit")
            {
                var applicationMessage = MessageFactory.CreateAppMessage(int.Parse(input), "ShiftUpdated");
                publisher.Publish(applicationMessage);

                input = Console.ReadLine();
            }
        }
    }
}
