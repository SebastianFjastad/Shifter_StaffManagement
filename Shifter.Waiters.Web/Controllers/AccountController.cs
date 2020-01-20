using System;
using Framework;
using Shifter.Service.Api.Requests;
using Shifter.Shared.WebClient.Actions;
using Shifter.Shared.WebClient.Extensions;
using Shifter.Shared.WebClient.ViewModels;
using Shifter.Waiters.Web.Attributes;
using System.Web.Mvc;

namespace Shifter.Waiters.Web.Controllers
{
    /// <summary>
    /// Represents the account controller for login, registration and forgot username/password
    /// </summary>
    public class AccountController : ShifterBaseController
    {
        public AccountController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        #region Edit Account

        [AuthorizeUserData]
        [HttpGet]
        public ActionResult PersonalDetails()
        {
            var waiterResponse = ServiceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest { StaffId = ResolveStaffId() });

            var waiter = waiterResponse.Staff;

            var viewModel = PersonalDetailsViewModel.Create(waiterResponse.Username, waiter.FirstName, waiter.LastName, waiter.EmailAddress, waiter.ContactNumber);

            return View(viewModel);
        }

        [AuthorizeUserData]
        [HttpPost]
        public ActionResult SavePersonalDetails(PersonalDetailsViewModel personalDetails)
        {
            var action = new SavePersonalDetailsAction<ActionResult>(ServiceRegistry.AuthenticationService)
            {
                OnComplete = (m) => View("PersonalDetails", m),
            };

            return action.Invoke(personalDetails, ResolveUserAccountId());
        }

        [AuthorizeUserData]
        [HttpPost]
        public JsonResult UpdateLastActiveDate()
        {
            var request = new UpdateLastActiveDateRequest() {Date = DateTime.Now, StaffMemberId = ResolveStaffId()};

            var result = ServiceRegistry.StaffService.UpdateLastActiveDate(request);

            return result.NotificationCollection.HasErrors() ? new JsonResult().Error() : new JsonResult().Successful();
        }

        #endregion
    }
}
