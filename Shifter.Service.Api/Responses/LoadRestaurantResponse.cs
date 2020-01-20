using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadRestaurantResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<RestaurantDto> Restaurants { get; set; }

        public LoadRestaurantResponse()
        {
            Restaurants = new List<RestaurantDto>();
        }
    }
}