using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IRestaurantDomainService
    {
        IEnumerable<Restaurant> LoadRestaurantsByManagerId(int managerId);

        IEnumerable<Restaurant> LoadRestaurantsByStaffId(int staffId);

        Restaurant LoadRestaurant(int restaurantId);

        NotificationCollection SaveRestaurant(Restaurant restaurant);
    }
}
