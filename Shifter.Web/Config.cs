using System.Configuration;

namespace Shifter.Web
{
    public static class Config
    {
        public static string ContactMeEmailAddress
        {
            get { return ConfigurationSettings.AppSettings["ContactMeEmailAddress"]; }
        }
    }
}
