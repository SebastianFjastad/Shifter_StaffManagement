using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class LoadManagerResponseExtensions
    {
        public static LoadManagerResponse AsLoadManagerResponse(this Manager manager)
        {
            Guard.InstanceNotNull(manager, "manager");

            var result = new LoadManagerResponse();
            
            var managerDto = MappingUtility.Map<Manager, ManagerDto>(manager);

            managerDto.FirstName = manager.UserAccount.FirstName;
            managerDto.LastName = manager.UserAccount.LastName;
            managerDto.EmailAddress = manager.UserAccount.EmailAddress;
            managerDto.ContactNumber = manager.UserAccount.ContactNumber;

            result.Manager = managerDto;
            result.Username = manager.UserAccount.Username;

            return result;
        }
    }
}
