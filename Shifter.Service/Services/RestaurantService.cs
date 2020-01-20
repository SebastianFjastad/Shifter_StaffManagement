using System;
using Framework;
using Shifter.Domain.Services;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Services
{
    public class RestaurantService : ShifterServiceBase, IRestaurantService
    {
        private readonly IRestaurantDomainService restaurantDomainService;

        public RestaurantService(IRestaurantDomainService restaurantDomainService)
        {
            Guard.ArgumentNotNull(restaurantDomainService, "restaurantDomainService");

            this.restaurantDomainService = restaurantDomainService;
        }

        public LoadRestaurantResponse LoadRestaurantByManager(LoadRestuarantByManagerRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var restaurants = restaurantDomainService.LoadRestaurantsByManagerId(request.ManagerId);

                return restaurants.AsLoadRestaurantResponse();
            });
        }

        public LoadRestaurantResponse LoadRestaurantByStaff(LoadRestaurantByStaffRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var restaurants = restaurantDomainService.LoadRestaurantsByStaffId(request.StaffId);

                return restaurants.AsLoadRestaurantResponse();
            });
        }

        public LoadRestaurantResponse LoadRestaurantById(LoadRestuarantByIdRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var restaurants = new[] { restaurantDomainService.LoadRestaurant(request.RestaurantId) };

                return restaurants.AsLoadRestaurantResponse();
            });
        }
    }
}