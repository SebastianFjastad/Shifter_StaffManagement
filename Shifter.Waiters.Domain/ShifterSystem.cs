using Framework;
using Shifter.Waiters.Domain.Services;

namespace Shifter.Waiters.Domain
{
    public class ShifterSystem : IShifterSystem
    {
        /// <summary>
        /// Gets the shift service implementation.
        /// </summary>
        public IShiftService ShiftService { get; private set; }

        /// <summary>
        /// Gets the waiter service implementation
        /// </summary>
        public IWaiterService WaiterService { get; private set; }

        /// <summary>
        /// Gets the restaurant service implementation
        /// </summary>
        public IRestaurantService RestaurantService { get; private set; }

        /// <summary>
        /// Defines the system to get hold of domain services
        /// </summary>
        public ShifterSystem() { }

        /// <summary>
        /// Defines the system to get hold of domain services
        /// </summary>
        public ShifterSystem(IShiftService shiftService, IWaiterService waiterService, IRestaurantService restaurantService)
        {
            Guard.ArgumentNotNull(shiftService, "shiftService");
            Guard.ArgumentNotNull(waiterService, "waiterService");
            Guard.ArgumentNotNull(restaurantService, "restaurantService");

            this.ShiftService = shiftService;
            this.WaiterService = waiterService;
            this.RestaurantService = restaurantService;
        }
    }
}
