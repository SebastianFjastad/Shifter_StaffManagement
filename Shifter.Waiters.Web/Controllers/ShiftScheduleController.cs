using Shifter.Shared.WebClient.Extensions;
using Shifter.Waiters.Web.Actions.Shifts;
using Shifter.Waiters.Web.Attributes;
using Shifter.Waiters.Web.Extensions;
using System;
using System.Web.Mvc;

namespace Shifter.Waiters.Web.Controllers
{
    [AuthorizeUserData]
    public class ShiftScheduleController : ShifterBaseController
    {
        public ShiftScheduleController(IServiceRegistry serviceRegistry) : base(serviceRegistry){}

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult LoadShiftsToTake(DateTime shiftDate, int? waiterId = null)
        {
            var action = new LoadShiftsAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("ShiftsToTake", model)
            };

            return action.Invoke(ResolveRestaurantId(), shiftDate, ResolveStaffId(), waiterId);
        }
        
        [HttpGet]
        public PartialViewResult LoadShiftsToUpdate(DateTime shiftDate)
        {
            var action = new LoadWaiterShiftsAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("ShiftsToUpdate", model)
            };

            return action.Invoke(ResolveRestaurantId(), shiftDate, ResolveStaffId());
        }

        [HttpPost]
        public JsonResult TakeShift(int shiftId, string clientKey)
        {
            var action = new TakeShiftAction<JsonResult>(ServiceRegistry)
            {
                OnSuccess = () => new JsonResult().Successful(),
                OnFail = (n) => new JsonResult().Error(n)
            };

            return action.Invoke(shiftId, ResolveStaffId(), ResolveRestaurantId(), clientKey);
        }

        [HttpPost]
        public JsonResult UpdateShiftAvailablity(int shiftId, bool makeAvailable, string clientKey)
        {
            var action = new UpdateShiftAction<JsonResult>(ServiceRegistry)
            {
                OnSuccess = () => new JsonResult().Successful(),
                OnFail = (n) => new JsonResult().Error(n)
            };

            return action.Invoke(shiftId, ResolveStaffId(), ResolveRestaurantId(), makeAvailable, clientKey);
        }

        /// <summary>
        /// Loads the available shifts schedule based on the selected week
        /// </summary>
        [HttpGet]
        public PartialViewResult LoadAvailableShiftSchedule(DateTime? fromDate, DateTime? toDate, int weekNumber = 1)
        {
            var action = new LoadAvailableShiftsScheduleAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("AvailableShifts", model)
            };

            return action.Invoke(ResolveStaffId(), ResolveRestaurantId(), fromDate, toDate, weekNumber);
        }

        /// <summary>
        /// Loads the shift schedule for a specific day
        /// </summary>
        [HttpGet]
        public PartialViewResult LoadAllShiftsSchedule(DateTime date)
        {
            var action = new LoadStaffWithShiftsAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("AllShifts", model)
            };

            return action.Invoke(ResolveStaffId(), ResolveRestaurantId(), date);
        }
    }
}
