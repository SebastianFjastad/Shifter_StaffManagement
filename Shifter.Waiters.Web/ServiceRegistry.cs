using Framework;
using Shifter.Service.Api.Services;

namespace Shifter.Waiters.Web
{
    public class ServiceRegistry : IServiceRegistry
    {
        public ICommsService CommsService { get; private set; }
        public IAuthenticationService AuthenticationService { get; private set; }
        public IManagerService ManagerService { get; private set; }
        public IRestaurantService RestaurantService { get; private set; }
        public ISettingsService SettingsService { get; private set; }
        public IStaffService StaffService { get; private set; }
        public IShiftTemplateService ShiftTemplateService { get; private set; }
        public IShiftTimeSlotService ShiftTimeSlotService { get; private set; }
        public IShiftService ShiftService { get; private set; }
        public IWallService WallService { get; private set; }

        public ServiceRegistry(IManagerService managerService, IRestaurantService restaurantService, ISettingsService settingsService, IStaffService staffService, IShiftTemplateService shiftTemplateService, IShiftTimeSlotService shiftTimeSlotService, IShiftService shiftService, ICommsService commsService, IAuthenticationService authenticationService, IWallService wallService)
        {
            Guard.ArgumentNotNull(commsService, "commsService");
            Guard.ArgumentNotNull(managerService, "managerService");
            Guard.ArgumentNotNull(restaurantService, "restaurantService");
            Guard.ArgumentNotNull(settingsService, "settingsService");
            Guard.ArgumentNotNull(staffService, "staffService");
            Guard.ArgumentNotNull(shiftTemplateService, "shiftTemplateService");
            Guard.ArgumentNotNull(shiftTimeSlotService, "shiftTimeSlotService");
            Guard.ArgumentNotNull(shiftService, "shiftService");
            Guard.ArgumentNotNull(authenticationService, "authenticationService");
            Guard.ArgumentNotNull(wallService, "wallService");

            ManagerService = managerService;
            RestaurantService = restaurantService;
            SettingsService = settingsService;
            StaffService = staffService;
            ShiftTemplateService = shiftTemplateService;
            ShiftTimeSlotService = shiftTimeSlotService;
            ShiftService = shiftService;
            CommsService = commsService;
            AuthenticationService = authenticationService;
            WallService = wallService;
        }
    }
}