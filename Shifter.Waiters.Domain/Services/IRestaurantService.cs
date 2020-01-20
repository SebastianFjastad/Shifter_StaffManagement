using System.Collections.Generic;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain.Services
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> LoadRestaurants(int waiterId);

        Restaurant LoadRestaurant(int restaurantId);
    }
}
