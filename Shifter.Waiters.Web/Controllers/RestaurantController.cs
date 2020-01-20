using Shifter.Waiters.Web.Actions;
using System.Web.Mvc;

namespace Shifter.Waiters.Web.Controllers
{
    [Authorize]
    public class RestaurantController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public RestaurantController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        [HttpGet]
        public ActionResult LoadRestaurants(int waiterId)
        {
            var action = new LoadRestaurantsAction<ActionResult>(ServiceRegistry)
            {
                OnOneRestarauntFound = (id) => RedirectToAction("SetRestaurant", new { restaurantId = id }),
                OnManyRestaurantsFound = (model) => View("SelectRestaurant", model),
                NoRestarauntsFound = () => View("Error")
            };

            return action.Invoke(waiterId);
        }

        /// <summary>
        /// Attempts to set the selected restaurant
        /// </summary>
        public ActionResult SetRestaurant(int restaurantId)
        {
            SetRestaurantId(restaurantId);

            if (ResolveRestaurantId() == restaurantId)
            {
                return RedirectToAction("Index", "ShiftSchedule");
            }

            return View("Error");
        }
    }
}
