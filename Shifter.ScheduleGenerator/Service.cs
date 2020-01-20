using System.Data;
using System.Data.SqlClient;
using log4net;
using System;
using System.ServiceProcess;
using System.Timers;

namespace Shifter.ScheduleGenerator
{
    public partial class Service : ServiceBase
    {
        private static readonly Timer timer = new Timer();
        private static readonly ILog log = LogManager.GetLogger("ScheduleGeneratorLog");

        public Service()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void OnStart(string[] args)
        {
            log.Info("Schedule generator service started...");

            timer.Elapsed += Process;
            timer.Interval = Config.ProcessInterval;
            timer.Enabled = true;
            timer.Start();
        }

        protected override void OnStop()
        {
            log.Info("Schedule generator service stopped...");
        }

        private static void Process(object sender, ElapsedEventArgs e)
        {
            log.Info("Begin processing...");

            timer.Stop();

            try
            {
                using (var processor = new Processor())
                {
                    processor.Process();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }

            timer.Start();

            log.Info("End processing...");
        }
    }
}
