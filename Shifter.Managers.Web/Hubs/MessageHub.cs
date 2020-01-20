using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml;
using Framework.Extensions;
using Framework.PubSub.Messages;
using Framework.PubSub.Msmq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Shifter.Managers.Domain;

namespace Shifter.Managers.Web.Hubs
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

        internal void RaiseEvent(string topic, object message)
        {
            if (Clients.IsNotNull())
            {
                Clients.All.raiseEvent(topic, message);
            }
        }
    }
}