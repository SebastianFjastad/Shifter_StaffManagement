using System.Configuration;

namespace Shifter.PublicationsService
{
    public static class Config
    {
        public static double ProcessInterval
        {
            get { return double.Parse(ConfigurationSettings.AppSettings["ProcessInterval"]); }
        }

        public static int ProcessThreshold
        {
            get { return int.Parse(ConfigurationSettings.AppSettings["ProcessThreshold"]); }
        }
    }
}
