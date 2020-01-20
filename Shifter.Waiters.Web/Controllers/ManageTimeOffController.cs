using System.Linq;
using Framework;
using Framework.Notifications;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Shared.WebClient.Extensions;
using Shifter.Waiters.Web.Actions.ManageTimeOff;
using Shifter.Waiters.Web.Attributes;
using Shifter.Waiters.Web.ViewModels.ManageTimeOff;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shifter.Waiters.Web.Controllers
{
    [AuthorizeUserData]
    public class ManageTimeOffController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public ManageTimeOffController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        [HttpGet]
        public ActionResult Index(NotificationCollection errors = null)
        {
            var result = ServiceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest() { StaffId = ResolveStaffId() });

            Guard.InstanceNotNull(result.Staff, "result.Staff");

            return View("Index", new ManageTimeOffViewModel { Unavailability = result.Staff.UnavailabilityRecords, Notifications = errors});
        }

        [HttpPost]
        public ActionResult DeleteTimeOff(int unavailabilityId)
        {
            var action = new DeleteTimeOffAction<ActionResult>(ServiceRegistry)
            {
                OnSuccess = () => RedirectToAction("Index"),
                OnFailure = (n) => Index(n)
            };

            return action.Invoke(unavailabilityId);
        }

        [HttpPost]
        public ActionResult SaveTimeOff(StaffUnavailabilityRecordDto unavailability)
        {
            var action = new SaveTimeOffAction<ActionResult>(ServiceRegistry)
            {
                OnSuccess = () => RedirectToAction("Index"),
                OnFailure = (n) => Index(n)
            };

            return action.Invoke(ResolveStaffId(), unavailability);
        }
    }
}
