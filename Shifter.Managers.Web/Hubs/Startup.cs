using Owin;
using Microsoft.Owin;
using Shifter.Managers.Domain.Messages;

[assembly: OwinStartup(typeof(Shifter.Managers.Web.Hubs.Startup))]
namespace Shifter.Managers.Web.Hubs
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