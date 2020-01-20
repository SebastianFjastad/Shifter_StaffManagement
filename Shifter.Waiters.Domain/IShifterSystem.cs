using Shifter.Waiters.Domain.Services;

namespace Shifter.Waiters.Domain
{
    public interface IShifterSystem
    {
        /// <summary>
        /// The shift service
        /// </summary>
        IShiftService ShiftService { get; }

        /// <summary>
        /// The waiter service
        /// </summary>
        IWaiterService WaiterService { get; }

        /// <summary>
        /// The restaurant service
        /// </summary>
        IRestaurantService RestaurantService { get; }
    }
}
