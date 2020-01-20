using Framework.Notifications;

namespace Shifter.Managers.Web.ViewModels
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