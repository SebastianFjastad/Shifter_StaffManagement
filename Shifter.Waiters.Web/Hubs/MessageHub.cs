using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace Shifter.Waiters.Web.Hubs
{
    [HubName("messageHub")]
    public class MessageHub : Hub
    {
        //proxy
    }

    public class MessageBroadcaster
    {
        private MessageBroadcaster(IHubConnectionContext clients)
        {
            Clients = clients;
        }

        public static MessageBroadcaster Instance { get { return instance.Value; } }

        private static readonly Lazy<MessageBroadcaster> instance = new Lazy<MessageBroadcaster>(() => new MessageBroadcaster(GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients));

        public IHubConnectionContext Clients { get; set; }

        internal void RaiseEvent(string topic, object message, string clientKey)
        {
            if (Clients.IsNotNull())
            {
                Clients.All.raiseEvent(topic, message, clientKey);
            }
        }
    }
}