using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shifter.Web.Startup))]
namespace Shifter.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
