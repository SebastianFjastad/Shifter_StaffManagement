using Shifter.Managers.Domain.Services;

namespace Shifter.Managers.Domain
{
    public class ShifterSystem : IShifterSystem
    {
        /// <summary>
        /// Gets the shift service implementation.
        /// </summary>
        public IShiftService ShiftService { get; private set; }

        /// <summary>
        /// Gets the ShiftTemplateService implementation
        /// </summary>
        public IShiftTemplateService ShiftTemplateService { get; private set; }

        /// <summary>
        /// Gets the ShiftTimeSlotService implementation
        /// </summary>
        public IShiftTimeSlotService ShiftTimeSlotService { get; private set; }

        /// <summary>
        /// Gets the waiter service implementation
        /// </summary>
        public IWaiterService WaiterService { get; private set; }

        /// <summary>
        /// Gets the restaurant service implementation
        /// </summary>
        public IRestaurantService RestaurantService { get; private set; }

        /// <summary>
        /// Gets the manager service implementation
        /// </summary>
        public IManagerService ManagerService { get; private set; }

        /// <summary>
        /// Gets the manager service implementation
        /// </summary>
        public ISettingsService SettingsService { get; private set; }

        /// <summary>
        /// Defines the system to get hold of domain services
        /// </summary>
        public ShifterSystem() { }

        /// <summary>
        /// Defines the system to get hold of domain services
        /// </summary>
        public ShifterSystem(IShiftService shiftService, IWaiterService waiterService, IShiftTemplateService shiftTemplateService, IShiftTimeSlotService shiftTimeSlotService, IRestaurantService restaurantService, IManagerService managerService, ISettingsService settingsService)
        {
            this.ShiftService = shiftService;
            this.WaiterService = waiterService;
            this.ShiftTimeSlotService = shiftTimeSlotService;
            this.ShiftTemplateService = shiftTemplateService;
            this.RestaurantService = restaurantService;
            this.ManagerService = managerService;
            this.SettingsService = settingsService;
        }
    }
}
