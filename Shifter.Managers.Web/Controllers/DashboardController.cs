using System.Collections.Generic;
using System.Linq;
using Framework.Notifications;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.Attributes;
using Shifter.Managers.Web.Extensions;
using Shifter.Managers.Web.Utils;
using Shifter.Service.Api.Dtos;
using System;
using System.Web.Mvc;
using Shifter.Service.Api.Requests;
using Shifter.Shared.WebClient.Extensions;

namespace Shifter.Managers.Web.Controllers
{
    [AuthorizeUserData]
    public class DashboardController : ShifterBaseController
    {
         /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public DashboardController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        public ActionResult Start()
        {
            var result = ServiceRegistry.SettingsService.LoadRestaurantNotifications(new GenericEntityRequest(ResolveRestaurantId()));

            var hasMessages = result.NotificationCollection.Any(n => n.Severity == NotificationSeverity.Information);

            if (hasMessages)
            {
                return Index();
            }

            return RedirectToAction("ManageSchedule", "ShiftSchedule");
        }

        public ActionResult Index()
        {
            var action = new LoadSettingsAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) => View("Index", model)
            };

            return action.Invoke(ResolveRestaurantId());
        }

        [HttpPost]
        public ActionResult SaveSettings(SettingsDto settings)
        {
            var action = new SaveSettingsAction<ActionResult>(ServiceRegistry)
            {
                OnFailure = (model) => View("Index", model),
                OnSuccess =  () => RedirectToAction("Index")
            };

            return action.Invoke(settings);
        }

        [HttpGet]
        public JsonResult CheckForMessages()
        {
            var action = new CheckForMessagesAction<JsonResult>(ServiceRegistry)
            {
                OnComplete = (numMessages) => new JsonResult().Message(numMessages)
            };

            return action.Invoke(ResolveRestaurantId());
        }

        [HttpGet]
        public PartialViewResult LoadWaiterHours(DateTime fromDate, DateTime toDate, string selectedStaffTypeIds, double hourlyRate = 0)
        {
            var ids = StringUtils.ConvertToList(selectedStaffTypeIds);

            var action = new LoadWaiterHoursAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (hoursCollection) => PartialView("WaiterHours", hoursCollection)
            };

            return action.Invoke(ResolveRestaurantId(), fromDate, toDate, hourlyRate, ids);
        }
    }
}
