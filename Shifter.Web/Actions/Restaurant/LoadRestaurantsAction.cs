using System;
using System.Linq;
using Framework;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Shared.WebClient.Models;
using Shifter.Web.ViewModels.Restaurants;

namespace Shifter.Web.Actions.Restaurant
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

        public T Invoke(UserData userData)
        {
            Guard.InstanceNotNull(OnManyRestaurantsFound, "OnManyRestaurantsFound");
            Guard.InstanceNotNull(OnOneRestarauntFound, "OnOneRestarauntFound");
            Guard.InstanceNotNull(NoRestarauntsFound, "NoRestarauntsFound");

            var viewModel = new RestaurantsViewModel();

            if (userData.ProfileType == ProfileType.Waiter)
            {
                var response = serviceRegistry.RestaurantService.LoadRestaurantByStaff(new LoadRestaurantByStaffRequest { StaffId = userData.ProfileId });
                viewModel.Restaurants = response.Restaurants;
            }
            else if (userData.ProfileType == ProfileType.Manager)
            {
                var response = serviceRegistry.RestaurantService.LoadRestaurantByManager(new LoadRestuarantByManagerRequest() { ManagerId = userData.ProfileId });
                viewModel.Restaurants = response.Restaurants;
            }

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