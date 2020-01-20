using Microsoft.Owin;
using Owin;
using Shifter.Waiters.Web.Hubs;

[assembly: OwinStartup(typeof(Startup))]
namespace Shifter.Waiters.Web.Hubs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            //Start processing messages
            MessageProcessor.Initialize();
        }
    }
}