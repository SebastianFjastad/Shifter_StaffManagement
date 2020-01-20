using System;
using System.Linq;
using Framework;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class LoadRestaurantsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadRestaurantsAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<RestaurantsViewModel, T> OnManyRestaurantsFound { get; set; }
        public Func<int, T> OnOneRestarauntFound { get; set; }
        public Func<T> NoRestarauntsFound { get; set; }

        #endregion

        #region Methods

        public T Invoke(int managerId)
        {
            Guard.InstanceNotNull(OnManyRestaurantsFound, "OnManyRestaurantsFound");
            Guard.InstanceNotNull(OnOneRestarauntFound, "OnOneRestarauntFound");
            Guard.InstanceNotNull(NoRestarauntsFound, "NoRestarauntsFound");

            var viewModel = new RestaurantsViewModel();
            var response = serviceRegistry.RestaurantService.LoadRestaurantByManager(new LoadRestuarantByManagerRequest
            {
                ManagerId = managerId
            });

            viewModel.Restaurants = response.Restaurants;

            if (viewModel.Restaurants.Count() > 1)
            {
                return OnManyRestaurantsFound.Invoke(viewModel);
            }

            if (viewModel.Restaurants.Count() == 1)
            {
                return OnOneRestarauntFound.Invoke(viewModel.Restaurants.First().Id);
            }

            return NoRestarauntsFound.Invoke();

        }

        #endregion
    }
}