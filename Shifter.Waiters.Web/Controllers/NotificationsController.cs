using System.Collections.Generic;
using Shifter.Shared.WebClient.Extensions;
using Shifter.Waiters.Web.Actions;
using Shifter.Waiters.Web.Attributes;
using Shifter.Waiters.Web.Extensions;
using System.Linq;
using System.Web.Mvc;

namespace Shifter.Waiters.Web.Controllers
{
    [AuthorizeUserData]
    public class NotificationsController : ShifterBaseController
    {
        public NotificationsController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckForMessages()
        {
            var action = new LoadWaiterNotificationsAction<JsonResult>(ServiceRegistry)
            {
                OnComplete = (model) => new JsonResult().Message(model.Notifications.Count())
            };

            return action.Invoke(ResolveRestaurantId(), ResolveStaffId());
        }

        [HttpPost]
        public JsonResult ConfirmNotificationsReceived(int notificationId, string notificationHint)
        {
            var action = new ConfirmNotificationsReceived<JsonResult>(ServiceRegistry)
            {
                OnFailure = (model) => new JsonResult().Error(),
                OnSuccess = () => new JsonResult().Successful()
            };

            var notifications = new Dictionary<int, string>();
            notifications.Add(notificationId, notificationHint);

            return action.Invoke(notifications);
        }

        [HttpPost]
        public ActionResult ConfirmAllNotificationsReceived(Dictionary<int, string> notifications)
        {
            var action = new ConfirmNotificationsReceived<ActionResult>(ServiceRegistry)
            {
                OnFailure = (errors) => View("Errors", errors),
                OnSuccess = () => RedirectToAction("Index")
            };

            return action.Invoke(notifications);
        }

        public PartialViewResult LoadNotifications()
        {
            var action = new LoadWaiterNotificationsAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("Notifications", model)
            };

            return action.Invoke(ResolveRestaurantId(), ResolveStaffId());
        }
    }
}
