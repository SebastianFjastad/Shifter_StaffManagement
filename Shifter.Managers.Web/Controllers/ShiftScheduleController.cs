using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.Attributes;
using Shifter.Managers.Web.Utils;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;
using Shifter.Shared.WebClient.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Shifter.Managers.Web.Controllers
{
    [AuthorizeUserData]
    public class ShiftScheduleController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public ShiftScheduleController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        /// <summary>
        /// Loads the shift schedule page with relevant filters
        /// </summary>
        [HttpGet]
        public ActionResult ManageSchedule(DateTime? startWeek)
        {
            var viewModel = new ShiftScheduleViewModel();
            var loadTimeSlotResponse = ServiceRegistry.ShiftTimeSlotService.LoadTimeSlots(new LoadTimeSlotCollectionRequest { RestaurantId = ResolveRestaurantId() });
            var staffTypesResponse = ServiceRegistry.StaffService.LoadStaffTypes();

            viewModel.TimeSlots = loadTimeSlotResponse.ShiftTimeslots;
            viewModel.StaffTypes = staffTypesResponse.StaffTypes;

            if (startWeek.IsNotNull() && startWeek < DateTime.Now)
            {
                viewModel.CurrentWeek = startWeek.Value;
            }

            return View("ShiftSchedule", viewModel);
        }

        /// <summary>
        /// Loads the shift schedule based on the filters
        /// </summary>
        [HttpGet]
        public PartialViewResult LoadShiftSchedule(string staffTypeIds, DateTime? weekFrom, DateTime? weekTo)
        {
            var action = new LoadShiftScheduleAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("ShiftScheduleResult", model)
            };

            var staffTypeIdList = StringUtils.ConvertToList(staffTypeIds);

            return action.Invoke(ResolveRestaurantId(), weekFrom, weekTo, staffTypeIdList);
        }

        /// <summary>
        /// Loads the selected shift summary
        /// </summary>
        [HttpGet]
        public PartialViewResult LoadShiftSummary(int shiftId)
        {
            var loadShiftResponse = ServiceRegistry.ShiftService.LoadShift(new GenericEntityRequest(shiftId));

            if (loadShiftResponse.Shift.IsNotNull())
            {
                var model = new ShiftSummaryViewModel(loadShiftResponse.Shift);

                return PartialView("ShiftSummary", model);
            }

            return new PartialViewResult();
        }

        /// <summary>
        /// Returns a list of shifts to edit with timeslots
        /// </summary>
        [HttpGet]
        public PartialViewResult EditShifts(int waiterId, DateTime date)
        {
            var action = new LoadWaitersShiftsForDateAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("EditShift", model)
            };

            return action.Invoke(ResolveRestaurantId(), waiterId, date);
        }

        /// <summary>
        /// Saves the list of edited shifts for the selected date and waiter
        /// </summary>
        [HttpPost]
        public ActionResult SaveShiftChanges(IEnumerable<EditedShift> shifts)
        {
            //on save close popup, if errors then show in new popup otherwise reload page
            var action = new SaveShiftsAction<ActionResult>(ServiceRegistry)
            {
                OnSuccess = () => new JsonResult() { Data = new { IsSuccessful = true } },
                OnError = (model) => PartialView("Errors", model)
            };

            return action.Invoke(shifts);
        }

        /// <summary>
        /// Saves the shift
        /// </summary>
        [HttpPost]
        public JsonResult SaveShift(TimeSpan startTime, TimeSpan endTime, DateTime shiftDate, int waiterId, string containerId)
        {
            var action = new SaveShiftAction<JsonResult>(ServiceRegistry)
            {
                OnComplete = (model) => new JsonResult() { Data = new { ShiftContainerId = containerId, Payload = this.RenderPartialViewToString("ShiftsResult", model) } }
            };

            return action.Invoke(startTime, endTime, shiftDate, waiterId, ResolveRestaurantId());
        }

        [HttpPost]
        public JsonResult CopySchedule(DateTime scheduleStartDate, DateTime scheduleEndDate, DateTime copyToStartDate, string staffTypeIds)
        {
            var action = new CopyScheduleAction<JsonResult>(ServiceRegistry)
            {
                OnError = (n) => new JsonResult().Error(n),
                OnWarning = () => new JsonResult().Warning(string.Empty),
                OnSuccess = () => new JsonResult().Successful()
            };

            var staffTypeIdList = StringUtils.ConvertToList(staffTypeIds);

            return action.Invoke(ResolveRestaurantId(), scheduleStartDate, scheduleEndDate, copyToStartDate, staffTypeIdList, false);
        }

        [HttpPost]
        public JsonResult ForceScheduleCopy(DateTime scheduleStartDate, DateTime scheduleEndDate, DateTime copyToStartDate, string staffTypeIds)
        {
            var action = new CopyScheduleAction<JsonResult>(ServiceRegistry)
            {
                OnError = (n) => new JsonResult().Error(n),
                OnSuccess = () => new JsonResult().Successful()
            };

            var staffTypeIdList = StringUtils.ConvertToList(staffTypeIds);

            return action.Invoke(ResolveRestaurantId(), scheduleStartDate, scheduleEndDate, copyToStartDate, staffTypeIdList, true);
        }

        /// <summary>
        /// Deletes the shift
        /// </summary>
        [HttpPost]
        public JsonResult DeleteShift(int shiftId, int waiterId, string containerId)
        {
            var action = new DeleteShiftAction<JsonResult>(ServiceRegistry)
            {
                OnComplete = (model) => new JsonResult() { Data = new { ShiftContainerId = containerId, Payload = this.RenderPartialViewToString("ShiftsResult", model) } }
            };

            return action.Invoke(shiftId, ResolveRestaurantId(), waiterId);
        }
    }
}
