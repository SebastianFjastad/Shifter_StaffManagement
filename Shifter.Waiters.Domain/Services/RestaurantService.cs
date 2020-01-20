using System.Linq;
using Framework.Domain;
using System.Collections.Generic;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain.Services
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

        public IEnumerable<Restaurant> LoadRestaurants(int waiterId)
        {
            return repository.FindBy<Restaurant>(r => r.Waiters.Any(m => m.Id == waiterId));
        }

        public Restaurant LoadRestaurant(int restaurantId)
        {
            return repository.FindById<Restaurant>(restaurantId);
        }

        #endregion
    }
}
