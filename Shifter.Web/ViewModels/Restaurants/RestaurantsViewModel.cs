using System.Collections.Generic;
using Shifter.Service.Api.Dtos;

namespace Shifter.Web.ViewModels.Restaurants
{
    public class RestaurantsViewModel : ShifterModelBase
    {
        #region Properties

        public IEnumerable<RestaurantDto> Restaurants { get; set; }

        #endregion
    }
}