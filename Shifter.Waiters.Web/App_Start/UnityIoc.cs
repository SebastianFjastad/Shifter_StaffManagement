using Microsoft.Practices.Unity;
using Shifter.Service.Api.Services;
using Spring.Context;
using Spring.Context.Support;
using System.Web.Mvc;
using Unity.Mvc3;

namespace Shifter.Waiters.Web
{
    public static class UnityIoc
    {
        private static readonly IApplicationContext applicationContext = ContextRegistry.GetContext();

        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            var serviceRegistry = ResolveServiceRegistry();
            container.RegisterInstance(serviceRegistry);

            return container;
        }

        private static IServiceRegistry ResolveServiceRegistry()
        {
            var managerService = applicationContext["managerService"] as IManagerService;
            var restaurantService = applicationContext["restaurantService"] as IRestaurantService;
            var settingsService = applicationContext["settingsService"] as ISettingsService;
            var staffService = applicationContext["staffService"] as IStaffService;
            var shiftTemplateService = applicationContext["shiftTemplateService"] as IShiftTemplateService;
            var shiftTimeSlotService = applicationContext["shiftTimeSlotService"] as IShiftTimeSlotService;
            var shiftService = applicationContext["shiftService"] as IShiftService;
            var commsService = applicationContext["commsService"] as ICommsService;
            var authenticationService = applicationContext["authenticationService"] as IAuthenticationService;
            var wallService = applicationContext["wallService"] as IWallService;

            return new ServiceRegistry(
                managerService,
                restaurantService,
                settingsService,
                staffService,
                shiftTemplateService,
                shiftTimeSlotService,
                shiftService,
                commsService,
                authenticationService,
                wallService);
        }
    }
}
