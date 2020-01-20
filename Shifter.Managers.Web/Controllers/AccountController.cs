using System.Web.Mvc;
using Shifter.Managers.Web.Attributes;
using Shifter.Service.Api.Requests;
using Shifter.Shared.WebClient.Actions;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.Controllers
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
        public ActionResult EditProfile()
        {
            var managerResponse = ServiceRegistry.ManagerService.LoadManager(new GenericEntityRequest(ResolveManagerId()));

            var manager = managerResponse.Manager;

            var viewModel = PersonalDetailsViewModel.Create(managerResponse.Username, manager.FirstName, manager.LastName, manager.EmailAddress, manager.ContactNumber);

            return View(viewModel);
        }

        [AuthorizeUserData]
        [HttpPost]
        public ActionResult SaveProfile(PersonalDetailsViewModel viewModel)
        {
            var action = new SavePersonalDetailsAction<ActionResult>(ServiceRegistry.AuthenticationService)
            {
                OnComplete = (m) => View("EditProfile", m)
            };

            return action.Invoke(viewModel, ResolveUserAccountId());
        }

        #endregion
    }
}
