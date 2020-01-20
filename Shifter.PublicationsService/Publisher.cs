using log4net;
using Shifter.Persistence.Data;
using System;
using System.ServiceProcess;
using System.Timers;

namespace Shifter.PublicationsService
{
    public partial class Publisher : ServiceBase
    {

        private static readonly Timer timer = new Timer();
        private static readonly ILog log = LogManager.GetLogger("PublicationsServiceLog");

        public Publisher()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += Process;
            timer.Interval = Config.ProcessInterval;
            timer.Enabled = true;
            timer.Start();
        }

        protected override void OnStop()
        {
            log.Info("Publication service stopped...");
        }

        private static void Process(object sender, ElapsedEventArgs e)
        {
            log.Info("Begin processing...");

            timer.Stop();

            try
            {
                using (var factory = new DatabaseFactory("Shifter.Domain"))
                {
                    using (var repo = new Repository(factory))
                    {
                        using (var processor = new Processor(repo))
                        {
                            processor.Process();
                        }
                    }
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
