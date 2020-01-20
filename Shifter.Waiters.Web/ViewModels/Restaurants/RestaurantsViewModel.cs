using System.Collections.Generic;
using Shifter.Service.Api.Dtos;

namespace Shifter.Waiters.Web.ViewModels
{
    public class RestaurantsViewModel : ViewModelBase
    {
        #region Properties

        public IEnumerable<RestaurantDto> Restaurants { get; set; }

        #endregion
    }
}