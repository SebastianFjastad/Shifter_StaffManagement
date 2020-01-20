using System.Configuration;

namespace Shifter.Waiters.Domain
{
    public static class Config
    {
        public static string FromEmailAddress
        {
            get { return ConfigurationSettings.AppSettings["FromEmailAddress"];  }
        }

        public static string MessageQueueName
        {
            get { return ConfigurationSettings.AppSettings["MessageQueueName"]; }
        }
    }
}
