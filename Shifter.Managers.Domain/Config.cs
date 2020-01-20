using System.Configuration;

namespace Shifter.Managers.Domain
{
    public static class Config
    {
        public static string FromEmailAddress
        {
            get { return ConfigurationSettings.AppSettings["FromEmailAddress"];  }
        }

        public static string EmailHost
        {
            get { return ConfigurationSettings.AppSettings["EmailHost"]; }
        }

        public static string MessageQueueName
        {
            get { return ConfigurationSettings.AppSettings["MessageQueueName"]; }
        }
    }
}
