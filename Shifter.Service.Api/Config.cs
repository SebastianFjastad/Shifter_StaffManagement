using System.Configuration;

namespace Shifter.Service.Api
{
    public static class Config
    {
        public static string WaiterAppUrl
        {
            get { return ConfigurationSettings.AppSettings["WaiterAppUrl"]; }
        }

        public static string ManagerAppUrl
        {
            get { return ConfigurationSettings.AppSettings["ManagerAppUrl"]; }
        }
    }
}
