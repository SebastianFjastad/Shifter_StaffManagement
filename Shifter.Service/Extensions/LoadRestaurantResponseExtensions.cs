using System.Collections.Generic;
using System.Linq;
using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class LoadRestaurantResponseExtensions
    {
        public static LoadRestaurantResponse AsLoadRestaurantResponse(this IEnumerable<Restaurant> restaurants)
        {
            Guard.InstanceNotNull(restaurants, "restaurants");

            var result = new LoadRestaurantResponse();

            var restuarantDtos = restaurants.Select(MappingUtility.Map<Restaurant, RestaurantDto>).ToList();

            result.Restaurants = restuarantDtos;

            return result;
        }
    }
}