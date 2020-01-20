using System.Linq;
using Framework;
using System;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Waiters.Web.ViewModels;

namespace Shifter.Waiters.Web.Actions
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

        public T Invoke(int waiterId)
        {
            Guard.InstanceNotNull(OnManyRestaurantsFound, "OnManyRestaurantsFound");
            Guard.InstanceNotNull(OnOneRestarauntFound, "OnOneRestarauntFound");
            Guard.InstanceNotNull(NoRestarauntsFound, "NoRestarauntsFound");

            var viewModel = new RestaurantsViewModel();
            var loadRestaurantByWaiterResponse = serviceRegistry.RestaurantService.LoadRestaurantByStaff(
                new LoadRestaurantByStaffRequest
                {
                    StaffId = waiterId
                });

            viewModel.Restaurants =
                loadRestaurantByWaiterResponse.Restaurants;

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