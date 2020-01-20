using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Owin;
using Shifter.Waiters.Web.Hubs;

[assembly: AssemblyTitle("Shifter.Web")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Shifter.Web")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: ComVisible(false)]
[assembly: Guid("d6a2b706-f348-4d40-a65f-51ec9f59b1cc")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: OwinStartup(typeof(Startup))]
