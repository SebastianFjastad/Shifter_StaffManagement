using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.Attributes;
using Shifter.Managers.Web.Utils;
using Shifter.Service.Api.Dtos;
using System.Web.Mvc;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Controllers
{
    [AuthorizeUserData]
    public class ShiftTimeslotController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public ShiftTimeslotController(IServiceRegistry serviceRegistry)
            : base(serviceRegistry)
        {
        }

        /// <summary>
        /// Loads the timeslot manager page
        /// </summary>
        [HttpGet]
        public ActionResult ManageTimeslots()
        {
            var result = ServiceRegistry.StaffService.LoadStaffTypes();
            return View("ManageTimeslots", result.StaffTypes);
        }

        /// <summary>
        /// Loads the timeslots
        /// </summary>
        [HttpGet]
        public PartialViewResult LoadTimeslots(int staffTypeId)
        {
            var action = new LoadTimeslotsAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("ShiftTimeslots", model)
            };

            return action.Invoke(ResolveRestaurantId(), staffTypeId);
        }

        /// <summary>
        /// Adds a new timeslot
        /// </summary>
        //[HttpPost]
        public JsonResult AddTimeslot(string startTime, string endTime, int staffTypeId)
        {
            var action = new SaveTimeslotAction<JsonResult>(ServiceRegistry)
            {
                OnComplete = (model) => new JsonResult() { Data = new { StaffTypeId = staffTypeId, Payload = this.RenderPartialViewToString("ShiftTimeslots", model) } }
            };

            return action.Invoke(startTime, endTime, ResolveRestaurantId(), staffTypeId);
        }

        /// <summary>
        /// Deletes the timeslot
        /// </summary>
        [HttpPost]
        public JsonResult DeleteTimeslot(ShiftTimeSlotDto timeslot)
        {
            var action = new DeleteTimeslotAction<JsonResult>(ServiceRegistry)
            {
                OnComplete = (model) => new JsonResult() { Data = new { StaffTypeId = timeslot.StaffTypeId, Payload = this.RenderPartialViewToString("ShiftTimeslots", model) } }
            };

            return action.Invoke(timeslot);
        }
    }
}
