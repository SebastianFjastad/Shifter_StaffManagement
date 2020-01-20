using System.Collections.Generic;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class RestaurantsViewModel : ShifterModelBase
    {
        #region Properties

        public IEnumerable<RestaurantDto> Restaurants { get; set; }

        #endregion
    }
}