using System.Collections.Generic;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Services
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> LoadRestaurants(int managerId);

        Restaurant LoadRestaurant(int restaurantId);
    }
}
