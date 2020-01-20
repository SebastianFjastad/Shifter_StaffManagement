using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;
using Shifter.Service.Utilities;

namespace Shifter.Service.Services
{
    public class ManagerService : ShifterServiceBase, IManagerService
    {
        private readonly IManagerDomainService managerDomainService;

        public ManagerService(IManagerDomainService managerDomainService)
        {
            Guard.ArgumentNotNull(managerDomainService, "managerDomainService");

            this.managerDomainService = managerDomainService;
        }

        public LoadManagerResponse LoadManagerByUsernameAndPassword(AuthenticationRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var manager = managerDomainService.LoadManager(request.Username, request.Password);

                return manager.AsLoadManagerResponse();
            });
        }

        public LoadManagerResponse FindManager(FindManagerRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var manager = managerDomainService.FindManager(request.Username);

                return manager.AsLoadManagerResponse();
            });
        }

        public LoadManagerResponse LoadManager(GenericEntityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var manager = managerDomainService.LoadManager(request.EntityId);

                return manager.AsLoadManagerResponse();
            });
        }

        public GenericServiceResponse ResetPassword(GenericEntityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = managerDomainService.ResetPassword(request.EntityId);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse UpdateManagerPassword(UpdateManagerPasswordRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = managerDomainService.UpdatePassword(request.ManagerId, request.Password);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse SaveManager(SaveManagerRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = managerDomainService.SaveManager(MappingUtility.Map<ManagerDto, Manager>(request.Manager));

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }
    }
}
