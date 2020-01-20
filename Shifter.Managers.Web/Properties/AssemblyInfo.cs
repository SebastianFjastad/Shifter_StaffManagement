using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Owin;
using Shifter.Managers.Web.Hubs;

[assembly: AssemblyTitle("Shifter.CMS.Web")]
[assembly: AssemblyProduct("Shifter.CMS.Web")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: ComVisible(false)]
[assembly: Guid("49517e4e-7d22-4e41-8700-7248d2878795")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: OwinStartup(typeof(Startup))]
