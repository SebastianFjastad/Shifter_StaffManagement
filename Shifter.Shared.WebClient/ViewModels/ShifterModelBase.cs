using System.Linq;
using Framework.Notifications;

namespace Shifter.Shared.WebClient.ViewModels
{
    public class ShifterModelBase
    {
        #region Constructor

        /// <summary>
        /// Base view model to bundle common functionality
        /// </summary>
        public ShifterModelBase()
        {
            this.Notifications = NotificationCollection.CreateEmpty();
        }

        #endregion

        #region Properties

        public NotificationCollection Notifications { get; set; }

        public NotificationCollection Errors
        {
            get
            {
                return NotificationCollection.Create(Notifications.Where(n => n.Severity == NotificationSeverity.Error).ToList());
            }
        }

        public NotificationCollection Messages
        {
            get
            {
                return NotificationCollection.Create(Notifications.Where(n => n.Severity == NotificationSeverity.Information).ToList());
            }
        }

        public NotificationCollection Warnings
        {
            get
            {
                return NotificationCollection.Create(Notifications.Where(n => n.Severity == NotificationSeverity.Information).ToList());
            }
        }

        public bool HasErrors
        {
            get
            {
                return this.Errors.Any();
            }
        }

        public bool HasMessages
        {
            get
            {
                return Messages.Any();
            }
        }

        public bool HasWarnings
        {
            get
            {
                return Warnings.Any();
            }
        }

        #endregion
    }
}