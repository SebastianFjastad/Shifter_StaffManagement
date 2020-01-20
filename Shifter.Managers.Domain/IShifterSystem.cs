using Shifter.Managers.Domain.Services;

namespace Shifter.Managers.Domain
{
    public interface IShifterSystem
    {
        /// <summary>
        /// The shift service
        /// </summary>
        IShiftService ShiftService { get; }

        /// <summary>
        /// The shift template service
        /// </summary>
        IShiftTemplateService ShiftTemplateService { get; }

        /// <summary>
        /// The shift timeslot service
        /// </summary>
        IShiftTimeSlotService ShiftTimeSlotService { get; }

        /// <summary>
        /// The waiter service
        /// </summary>
        IWaiterService WaiterService { get; }

        /// <summary>
        /// The restaurant service
        /// </summary>
        IRestaurantService RestaurantService { get; }

        /// <summary>
        /// The manager service
        /// </summary>
        IManagerService ManagerService { get; }

        /// <summary>
        /// The Settings service
        /// </summary>
        ISettingsService SettingsService { get; }
    }
}
