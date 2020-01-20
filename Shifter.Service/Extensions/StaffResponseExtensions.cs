using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class StaffResponseExtensions
    {
        public static StaffResponse AsStaffResponse(this Staff staff)
        {
            Guard.InstanceNotNull(staff, "staff");

            var result = new StaffResponse();

            result.Staff = staff.AsStaffDto();
            result.Username = staff.UserAccount.Username;

            return result;
        }
    }
}