using Shifter.Service.Api.Requests;
using Shifter.Waiters.Web.Actions;
using System.Web.Mvc;
using Shifter.Waiters.Web.Attributes;
using Shifter.Waiters.Web.ViewModels;

namespace Shifter.Waiters.Web.Controllers
{
    [AuthorizeUserData]
    public class FeedbackController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public FeedbackController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        [HttpGet]
        public ActionResult Index()
        {
            return View("FeedbackForm", new FeedbackViewModel());
        }

        /// <summary>
        /// Sends feedback
        /// </summary>
        [HttpPost]
        public ActionResult SendFeedback(string title, string message)
        {
            var result = ServiceRegistry.CommsService.SaveFeedback(new SendFeedbackRequest() { Title = title, Message = message, UserAccountId = ResolveUserAccountId() });

            var viewModel = new FeedbackViewModel();
            viewModel.Notifications = result.NotificationCollection;

            return View("FeedbackForm", viewModel);
        }
    }
}
