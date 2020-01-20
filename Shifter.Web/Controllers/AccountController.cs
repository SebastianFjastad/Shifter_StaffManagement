using System.Net;
using Framework;
using Framework.Email;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Newtonsoft.Json;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Web.Actions.Account;
using Shifter.Web.Actions.Restaurant;
using Shifter.Web.Attributes;
using Shifter.Web.Extensions;
using Shifter.Web.Helpers;
using Shifter.Web.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Shifter.Web.ViewModels.ContactUs;
using Recaptcha;

namespace Shifter.Web.Controllers
{
    [Authorize]
    public class AccountController : ShifterBaseController
    {
        public AccountController(IServiceRegistry serviceRegistry)
            : base(serviceRegistry)
        {
        }

        #region Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            var url = ResolveAppUrl();

            if (url.IsNotNullOrEmpty())
            {
                //redirect to site
                return Redirect(url);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var request = new AuthenticationRequest
                {
                    Username = model.Username,
                    Password = model.Password
                };

                var response = ServiceRegistry.AuthenticationService.Authenticate(request);

                if (response.UserAccountId.HasValue)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    SetUserAccountId(response.UserAccountId.Value);
                }

                if (response.Profiles.IsNotNull())
                {
                    if (response.Profiles.Count() == 1)
                    {
                        var profile = response.Profiles.First();

                        SetUserProfile(profile);

                        return LoadRestaurants();
                    }

                    if (response.Profiles.Count() > 1)
                    {
                        return SelectProfiles(response.Profiles);
                    }
                }

                ModelState.AddModelError("", "Sorry we cant find you, please make sure your username and password are correct.");
            }

            return View(model);
        }

        /// <summary>
        /// Signs the user out and redirect to the login screen
        /// </summary>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        #endregion

        #region Register

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            var model = new RegistrationViewModel();

            return View(model);
        }

        [HttpPost]
        [AuthorizeReCapture]
        [AllowAnonymous]
        public ActionResult RegisterCompany(RegistrationViewModel model)
        {
            if (ModelState.ContainsKey("InvalidCaptcha"))
            {
                model.Notifications.AddError(ModelState["InvalidCaptcha"].Errors.First().ErrorMessage);
            }

            var request = new RegistrationRequest()
            {
                CompanyAddress = model.CompanyAddress,
                CompanyName = model.CompanyName,
                ManagerEmailAddress = model.ManagerEmailAddress,
                ManagerFirstName = model.ManagerFirstName,
                ManagerLastName = model.ManagerLastName,
                ManagerUsername = model.ManagerUsername,
                ManagerPassword = model.ManagerPassword
            };
            if (ModelState.IsValid)
            {
                var result = ServiceRegistry.AuthenticationService.Register(request);
                model.Notifications = result.NotificationCollection;
            }

            if (model.HasErrors || !ModelState.IsValid)
            {
                return View("Registration", model);
            }

            return Login(new LoginViewModel { Password = model.ManagerPassword, Username = model.ManagerUsername }, string.Empty);
        }

        #endregion

        #region Restaurant

        [HttpGet]
        public ActionResult LoadRestaurants()
        {
            var action = new LoadRestaurantsAction<ActionResult>(ServiceRegistry)
                         {
                             OnOneRestarauntFound = (id) => SetRestaurant(id),
                             OnManyRestaurantsFound = (model) => View("SelectRestaurant", model),
                             NoRestarauntsFound = () => View("Error")
                         };

            var userData = ResolveUserData();

            return action.Invoke(userData);
        }

        /// <summary>
        /// Attempts to set the selected restaurant
        /// </summary>
        public ActionResult SetRestaurant(int restaurantId)
        {
            SetRestaurantId(restaurantId);

            if (ResolveRestaurantId() == restaurantId)
            {
                var url = ResolveAppUrl();
                //redirect to site
                return Redirect(url);
            }

            return View("Error");
        }

        #endregion

        #region Edit Profile

        public ActionResult SelectProfiles(IEnumerable<ProfileSummary> profiles)
        {
            return View("SelectProfiles", new ProfilesViewModel(profiles));
        }

        public ActionResult SetProfile(int profileId, ProfileType profileType, string url)
        {
            SetUserProfile(new ProfileSummary(profileId) { ProfileType = profileType, AppUrl = url });

            return LoadRestaurants();
        }

        #endregion

        #region Forgot password

        [HttpGet]
        [AllowAnonymous]
        public ActionResult FindMeForm()
        {
            return View("ResetPassword");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string userAccountId)
        {
            var action = new ResetPasswordAction<ActionResult>(ServiceRegistry)
                         {
                             OnComplete = (m) => View("ResetPasswordResult", m)
                         };

            return action.Invoke(userAccountId);
        }

        [AllowAnonymous]
        public JsonResult FindMe(string username)
        {
            var action = new FindMeAction<JsonResult>(ServiceRegistry)
                         {
                             OnSuccess = (result) => new JsonResult().Successful(result),
                             OnFail = (message) => new JsonResult().Error(message)
                         };

            return action.Invoke(username);
        }

        #endregion

        #region ContactUs

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ContactMe(ContactUsViewModel viewModel)
        {
            try
            {
                var client = EmailUtils.CreateSmtpClient();

                var message = viewModel.GetFormattedMessage();

                var email = EmailUtils.CreateMailMessage("Contact me", message, viewModel.Email, Config.ContactMeEmailAddress);

                client.Send(email);

                return new JsonResult().Successful();
            }
            catch (Exception ex)
            {
                return new JsonResult().Error("Sorry your request could not be sent, please make sure your email address is correct.");
            }
        }

        #endregion
    }
}