using Shifter.Shared.WebClient.Extensions;
using System.Web.Mvc;

namespace Shifter.Waiters.Web.Controllers
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

        protected int ResolveStaffId()
        {
            var userData = HttpContext.GetUserData();

            return userData.ProfileId;
        }

        protected int ResolveUserAccountId()
        {
            var userData = HttpContext.GetUserData();

            return userData.UserAccountId;
        }

        #endregion
    }

    
}