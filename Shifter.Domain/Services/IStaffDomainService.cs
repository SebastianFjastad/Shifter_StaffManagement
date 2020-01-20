using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IStaffDomainService
    {
        int LoadTotalStaff(int restaurantId);

        IEnumerable<Staff> LoadStaff(int restaurantId, IEnumerable<int> staffTypeIds);

        IEnumerable<StaffType> LoadStaffTypes();

        Staff LoadStaffMember(int staffId);

        Staff LoadStaff(string username, string password);

        NotificationCollection SaveStaffUnavailability(IEnumerable<StaffUnavailabilityRecord> unavailability);

        NotificationCollection DeleteStaffUnavailability(IEnumerable<int> unavailabilityIds);

        NotificationCollection SaveStaff(Staff staff, UserAccount userAccount, bool emailAddressIsActive = true);

        NotificationCollection DeleteStaff(int staffId);

        NotificationCollection ResetPassword(int staffId);

        string GenerateUsername(string firstName, string lastName, string restaurantName);
    }
}
