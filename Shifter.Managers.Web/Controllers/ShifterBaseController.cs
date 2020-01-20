using Shifter.Shared.WebClient.Extensions;
using System.Web.Mvc;

namespace Shifter.Managers.Web.Controllers
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
        
        protected int ResolveRestaurantId()
        {
            var userData = HttpContext.GetUserData();

            return userData.RestaurtantId;
        }

        protected int ResolveManagerId()
        {
            var userData = HttpContext.GetUserData();

            return userData.ProfileId;
        }

        protected int ResolveUserAccountId()
        {
            var userData = HttpContext.GetUserData();

            return userData.UserAccountId;
        }
    }
}