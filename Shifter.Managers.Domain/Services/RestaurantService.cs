using Framework.Domain;
using Shifter.Managers.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Managers.Domain.Services
{
    public class RestaurantService : IRestaurantService
    {
        #region Constructors

        public RestaurantService (IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        #region Implementation of IRestaurantService

        public IEnumerable<Restaurant> LoadRestaurants(int managerId)
        {
            return repository.FindBy<Restaurant>(r => r.Managers.Any(m => m.Id == managerId));
        }

        public Restaurant LoadRestaurant(int restaurantId)
        {
            return repository.FindById<Restaurant>(restaurantId);
        }

        #endregion
    }
}
