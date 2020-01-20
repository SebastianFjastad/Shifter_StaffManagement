using Framework;
using Shifter.Service.Api.Services;

namespace Shifter.Web
{
    public class ServiceRegistry : IServiceRegistry
    {
        public IAuthenticationService AuthenticationService { get; private set; }
        public IManagerService ManagerService { get; private set; }
        public IRestaurantService RestaurantService { get; private set; }
        public IStaffService StaffService { get; private set; }

        public ServiceRegistry(IManagerService managerService, IRestaurantService restaurantService, IStaffService staffService, IAuthenticationService authenticationService)
        {
            Guard.ArgumentNotNull(authenticationService, "authenticationService");
            Guard.ArgumentNotNull(managerService, "managerService");
            Guard.ArgumentNotNull(restaurantService, "restaurantService");
            Guard.ArgumentNotNull(staffService, "staffService");

            AuthenticationService = authenticationService;
            ManagerService = managerService;
            RestaurantService = restaurantService;
            StaffService = staffService;
        }
    }
}