using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shifter.GroupAdmins.Web.Startup))]
namespace Shifter.GroupAdmins.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
