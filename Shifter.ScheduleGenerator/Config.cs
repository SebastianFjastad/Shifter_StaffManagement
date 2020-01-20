using System;
using System.Configuration;

namespace Shifter.ScheduleGenerator
{
    public static class Config
    {
        public static double ProcessInterval
        {
            get { return double.Parse(ConfigurationSettings.AppSettings["ProcessInterval"]); }
        }

        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ShifterConnection"].ConnectionString; }
        }

        public static DayOfWeek GenerationDay
        {
            get { return (DayOfWeek) Enum.Parse(typeof (DayOfWeek), ConfigurationSettings.AppSettings["GenerationDay"]); }
        }

        public static int GenerationFromHour
        {
            get { return int.Parse(ConfigurationSettings.AppSettings["GenerationFromHour"]); }
        }

        public static int GenerationToHour
        {
            get { return int.Parse(ConfigurationSettings.AppSettings["GenerationToHour"]); }
        }

        public static int StartWeek
        {
            get { return int.Parse(ConfigurationSettings.AppSettings["StartWeek"]); }
        }
    }
}
