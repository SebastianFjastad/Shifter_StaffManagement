using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.Extensions;
using Shifter.Shared.WebClient.Models;

namespace Shifter.Web.Controllers
{
    public abstract class ShifterBaseController : Controller
    {
        #region Constructor

        protected ShifterBaseController(IServiceRegistry serviceRegistry)
        {
            ServiceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        protected readonly IServiceRegistry ServiceRegistry;

        #endregion

        #region Methods

        protected int ResolveRestaurantId()
        {
            var userData = HttpContext.GetUserData();

            return userData.RestaurtantId;
        }

        protected string ResolveAppUrl()
        {
            var userData = HttpContext.GetUserData();

            return userData.AppUrl;
        }

        protected UserData ResolveUserData()
        {
            var userData = HttpContext.GetUserData();

            return userData;
        }

        protected ProfileType? ResolveProfileType()
        {
            var userData = HttpContext.GetUserData();

            return userData.ProfileType;
        }

        public void SetUserProfile(ProfileSummary profile)
        {
            //Implement strategies if number of profile types increases

            if (profile.IsNotNull())
            {
                var userData = HttpContext.GetUserData();

                userData.ProfileType = profile.ProfileType;
                userData.AppUrl = profile.AppUrl;
                userData.ProfileId = profile.ProfileId;

                HttpContext.UpdateCookie(userData);
            }
        }

        protected void SetRestaurantId(int restaurantId)
        {
            var userData = HttpContext.GetUserData();

            userData.RestaurtantId = restaurantId;

            HttpContext.UpdateCookie(userData);
        }

        protected void SetUserAccountId(int userAccountId)
        {
            var userData = HttpContext.GetUserData();

            userData.UserAccountId = userAccountId;

            HttpContext.UpdateCookie(userData);
        }

        #endregion
    }
}