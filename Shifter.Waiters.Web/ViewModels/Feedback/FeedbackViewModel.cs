using Framework.Notifications;

namespace Shifter.Waiters.Web.ViewModels
{
    public class FeedbackViewModel
    {
        public FeedbackViewModel()
        {
            Notifications = NotificationCollection.CreateEmpty();
        }

        #region Properties

        public NotificationCollection Notifications { get; set; }

        #endregion
    }
}