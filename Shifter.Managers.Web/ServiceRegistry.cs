using Framework;
using Shifter.Service.Api.Services;

namespace Shifter.Managers.Web
{
    public class ServiceRegistry : IServiceRegistry
    {
        public IAuthenticationService AuthenticationService { get; private set; }
        public ICommsService CommsService { get; private set; }
        public IManagerService ManagerService { get; private set; }
        public IRestaurantService RestaurantService { get; private set; }
        public ISettingsService SettingsService { get; private set; }
        public IStaffService StaffService { get; private set; }
        public IWallService WallService { get; private set; }
        public IShiftTemplateService ShiftTemplateService { get; private set; }
        public IShiftTimeSlotService ShiftTimeSlotService { get; private set; }
        public IShiftService ShiftService { get; private set; }

        public ServiceRegistry(IManagerService managerService, IRestaurantService restaurantService, ISettingsService settingsService, IStaffService staffService, IWallService wallService, IShiftTemplateService shiftTemplateService, IShiftTimeSlotService shiftTimeSlotService, IShiftService shiftService, ICommsService commsService, IAuthenticationService authenticationService)
        {
            Guard.ArgumentNotNull(commsService, "commsService");
            Guard.ArgumentNotNull(managerService, "managerService");
            Guard.ArgumentNotNull(restaurantService, "restaurantService");
            Guard.ArgumentNotNull(settingsService, "settingsService");
            Guard.ArgumentNotNull(staffService, "staffService");
            Guard.ArgumentNotNull(wallService, "wallService");
            Guard.ArgumentNotNull(shiftTemplateService, "shiftTemplateService");
            Guard.ArgumentNotNull(shiftTimeSlotService, "shiftTimeSlotService");
            Guard.ArgumentNotNull(shiftService, "ShiftService");
            Guard.ArgumentNotNull(authenticationService, "authenticationService");

            ManagerService = managerService;
            RestaurantService = restaurantService;
            SettingsService = settingsService;
            StaffService = staffService;
            WallService = wallService;
            ShiftTemplateService = shiftTemplateService;
            ShiftTimeSlotService = shiftTimeSlotService;
            ShiftService = shiftService;
            CommsService = commsService;
            AuthenticationService = authenticationService;
        }
    }
}