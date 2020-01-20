using Shifter.Service.Api.Services;

namespace Shifter.Web
{
    public interface IServiceRegistry
    {
        IAuthenticationService AuthenticationService { get; }

        IManagerService ManagerService { get; }

        IRestaurantService RestaurantService { get; }

        IStaffService StaffService { get; }
    }
}