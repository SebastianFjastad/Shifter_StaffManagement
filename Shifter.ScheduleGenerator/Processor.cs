using System;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace Shifter.ScheduleGenerator
{
    public class Processor : IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger("ScheduleGeneratorLog");

        public Processor()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Dispose()
        {
            
        }

        public void Process()
        {
            var now = DateTime.Now.TimeOfDay;
            var fromTime = new TimeSpan(Config.GenerationFromHour, 0, 0);
            var toTime = new TimeSpan(Config.GenerationToHour, 0, 0);

            if (DateTime.Now.DayOfWeek == Config.GenerationDay && now > fromTime && now < toTime)
            {
                var conn = new SqlConnection(Config.ConnectionString);

                try
                {
                    var cmd = new SqlCommand("GenerateShifts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@StartWeek", Config.StartWeek);
                    cmd.Parameters.AddWithValue("@RestaurantId", DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
