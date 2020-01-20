using Shifter.Service.Api.Services;

namespace Shifter.GroupAdmins.Web
{
    public interface IServiceRegistry
    {
        IManagerService ManagerService { get; }

        IRestaurantService RestaurantService { get; }

        ISettingsService SettingsService { get; }

        IStaffService StaffService { get; }

        IShiftTemplateService ShiftTemplateService { get; }
        
        IShiftTimeSlotService ShiftTimeSlotService { get; }

        IShiftService ShiftService { get; }

        ICommsService CommsService { get; }
    }
}