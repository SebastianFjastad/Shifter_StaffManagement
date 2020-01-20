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
    public class GroupAdminService : ShifterServiceBase, IGroupAdminService
    {
        public GroupAdminService()
        {
            
        }

        public LoadManagerResponse LoadGroupAdminByUsernameAndPassword(AuthenticationRequest request)
        {
            return null;
        }
    }
}
