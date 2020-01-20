using System;
using Framework.Notifications;
using Framework.Services;
using log4net;

namespace Shifter.Service.Services
{
    public abstract class ShifterServiceBase
    {
        protected readonly ILog Logger = LogManager.GetLogger("ServiceLog");

        protected ShifterServiceBase()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected T TryExecute<T>(Func<T> func) where T : IServiceResponse, new()
        {
            var result = new T();

            try
            {
                result = func();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                result.NotificationCollection.AddMessage(new Notification("Sorry an unexpected error occurred.", NotificationSeverity.Error));
            }

            return result;
        }
    }
}