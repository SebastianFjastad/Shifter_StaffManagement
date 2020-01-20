using System.Collections.Generic;
using System.Linq;
using Framework.Domain;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public class RestaurantDomainService : IRestaurantDomainService
    {
        #region Constructors

        public RestaurantDomainService (IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        #region Implementation of IRestaurantDomainService

        public IEnumerable<Restaurant> LoadRestaurantsByManagerId(int managerId)
        {
            return repository.FindBy<Restaurant>(r => r.Managers.Any(m => m.Id == managerId));
        }

        public IEnumerable<Restaurant> LoadRestaurantsByStaffId(int staffId)
        {
            return repository.FindBy<Restaurant>(r => r.Staff.Any(m => m.Id == staffId));
        }

        public Restaurant LoadRestaurant(int restaurantId)
        {
            return repository.FindById<Restaurant>(restaurantId);
        }

        public NotificationCollection SaveRestaurant(Restaurant restaurant)
        {
            return repository.Save(restaurant);
        }

        #endregion
    }
}
