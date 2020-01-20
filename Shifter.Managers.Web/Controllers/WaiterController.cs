using System;
using Framework.Notifications;
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.Actions.Waiter;
using Shifter.Managers.Web.Attributes;
using Shifter.Managers.Web.Extensions;
using Shifter.Managers.Web.ViewModels;
using System.Web.Mvc;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.Extensions;

namespace Shifter.Managers.Web.Controllers
{
    [AuthorizeUserData]
    public class WaiterController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public WaiterController(IServiceRegistry serviceRegistry)
            : base(serviceRegistry)
        {

        }

        /// <summary>
        /// Loads all staff
        /// </summary>
        public ActionResult ManageWaiters()
        {
            var action = new LoadStaffListAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) => View("ManageWaiters", model)
            };

            return action.Invoke(ResolveRestaurantId());
        }

        /// <summary>
        /// Loads an individual staff member
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var action = new LoadStaffMemberAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) => View("EditStaffMember", model)
            };
            return action.Invoke(ResolveRestaurantId(), id);
        }

        /// <summary>
        /// Loads an individual staff member
        /// </summary>
        [HttpGet]
        public ActionResult EditFailed(int? id, NotificationCollection errors)
        {
            var action = new LoadStaffMemberAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) =>
                {
                    model.Notifications = errors;
                    return View("EditStaffMember", model);
                }
            };
            return action.Invoke(ResolveRestaurantId(), id);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var action = new LoadStaffMemberAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) => View("EditStaffMember", model)
            };
            return action.Invoke(ResolveRestaurantId());
        }

        /// <summary>
        /// Attempts to register the waiter
        /// </summary>
        [HttpPost]
        public ActionResult SaveWaiter(StaffMemberViewModel viewModel)
        {
            var action = new SaveWaiterAction<ActionResult>(ServiceRegistry)
            {
                OnSuccess = () => RedirectToAction("ManageWaiters"),
                OnFailure = (errors) => EditFailed(viewModel.Staff.Id, errors)
            };

            return action.Invoke(viewModel, ResolveRestaurantId());
        }

        /// <summary>
        /// Attempts to delete the waiter
        /// </summary>
        [HttpPost]
        public ActionResult DeleteWaiter(int id)
        {
            var action = new DeleteWaiterAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = () => RedirectToAction("ManageWaiters"),
                OnFailure = (errors) => EditFailed(id, errors)
            };

            return action.Invoke(id, ResolveRestaurantId());
        }

        /// <summary>
        /// Resets the waiters password
        /// </summary>
        [HttpPost]
        public JsonResult ResetPassword(int id)
        {
            var action = new ResetPasswordAction<JsonResult>(ServiceRegistry)
            {
                OnSuccess = () => new JsonResult().Successful(),
                OnFailure = () => new JsonResult().Error()
            };

            return action.Invoke(id);
        }

        public ActionResult ManageStaffMemberLeave(int staffMemberId, NotificationCollection notifications = null)
        {
            var action = new LoadStaffMemberAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) =>
                {
                    model.ShowLeave = true;
                    if (notifications.IsNotNull())
                    {
                        model.LeaveErrors = notifications;
                    }
                    return View("EditStaffMember", model);
                }
            };

            return action.Invoke(ResolveRestaurantId(), staffMemberId);
        }

        [HttpPost]
        public ActionResult DeleteTimeOff(int unavailabilityId, int staffMemberId)
        {
            var action = new DeleteTimeOffAction<ActionResult>(ServiceRegistry)
            {
                OnSuccess = () => ManageStaffMemberLeave(staffMemberId),
                OnFailure = (n) => ManageStaffMemberLeave(staffMemberId, n)
            };

            return action.Invoke(unavailabilityId);
        }

        [HttpPost]
        public ActionResult SaveTimeOff(StaffUnavailabilityRecordDto unavailability, int staffMemberId)
        {
            var action = new SaveTimeOffAction<ActionResult>(ServiceRegistry)
            {
                OnSuccess = () => ManageStaffMemberLeave(staffMemberId),
                OnFailure = (n) => ManageStaffMemberLeave(staffMemberId, n)
            };

            return action.Invoke(staffMemberId, unavailability);
        }
    }
}
