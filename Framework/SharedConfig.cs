using System.Configuration;

namespace Framework
{
    public static class SharedConfig
    {
        public static string MessageQueueName
        {
            get { return ConfigurationSettings.AppSettings["MessageQueueName"]; }
        }

        public static string FromSupportEmailAddress
        {
            get { return ConfigurationSettings.AppSettings["FromEmailAddress"]; }
        }

        public static string FeedbackToEmailAddress
        {
            get { return ConfigurationSettings.AppSettings["FeedbackToEmailAddress"]; }
        }

        public static string SMTPHost
        {
            get { return ConfigurationSettings.AppSettings["SMTPHost"]; }
        }

        public static string SMTPEmailUsername
        {
            get { return ConfigurationSettings.AppSettings["SMTPEmailUsername"]; }
        }

        public static string SMTPEmailPassword
        {
            get { return ConfigurationSettings.AppSettings["SMTPEmailPassword"]; }
        }

        public static bool SMTPEnableSsl
        {
            get { return bool.Parse(ConfigurationSettings.AppSettings["SMTPEnableSsl"]); }
        }

        public static string LoginUrl
        {
            get { return ConfigurationSettings.AppSettings["LoginUrl"]; }
        }

        public static string LogoutUrl
        {
            get { return ConfigurationSettings.AppSettings["LogoutUrl"]; }
        }
    }
}
