using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.Attributes;
using Shifter.Service.Api.Requests;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Shifter.Managers.Web.Controllers
{
    [Authorize]
    public class RestaurantController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public RestaurantController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        [HttpGet]
        public ActionResult LoadRestaurants(int managerId)
        {
            var action = new LoadRestaurantsAction<ActionResult>(ServiceRegistry)
            {
                OnOneRestarauntFound = (id) => RedirectToAction("SetRestaurant", new { restaurantId = id }),
                OnManyRestaurantsFound = (model) => View("SelectRestaurant", model),
                NoRestarauntsFound = () => View("Error")
            };

            return action.Invoke(managerId);
        }

        /// <summary>
        /// Attempts to set the selected restaurant
        /// </summary>
        public ActionResult SetRestaurant(int restaurantId)
        {
            var response = ServiceRegistry.RestaurantService.LoadRestaurantById(new LoadRestuarantByIdRequest {RestaurantId = restaurantId});

            if (response.IsNotNull() && response.Restaurants.IsNotNull() && response.Restaurants.Any(r => r.Id == restaurantId))
            {
                SetRestaurantId(restaurantId);
            }

            if (ResolveRestaurantId() == restaurantId)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View("Error");
        }
    }
}
