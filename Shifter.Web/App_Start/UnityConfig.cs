using Microsoft.Practices.Unity;
using Shifter.Service.Api.Services;
using Shifter.Web;
using Spring.Context;
using Spring.Context.Support;
using System.Web.Mvc;
using Unity.Mvc5;

namespace Shifter.Waiters.Web
{
    public static class UnityConfig
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
            var authenticationService = applicationContext["authenticationService"] as IAuthenticationService;
            var managerService = applicationContext["managerService"] as IManagerService;
            var restaurantService = applicationContext["restaurantService"] as IRestaurantService;
            var staffService = applicationContext["staffService"] as IStaffService;

            return new ServiceRegistry(
                managerService,
                restaurantService,
                staffService,
                authenticationService);
        }
    }
}
