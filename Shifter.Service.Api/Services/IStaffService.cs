using System.ServiceModel;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface IStaffService
    {
        [OperationContract]
        StaffCollectionResponse LoadStaffList(LoadStaffListRequest request);
        
        [OperationContract]
        StaffTypeCollectionResponse LoadStaffTypes();

        [OperationContract]
        StaffHoursResponse LoadStaffHours(LoadStaffHoursRequest request);

        [OperationContract]
        StaffResponse LoadStaffMember(LoadStaffMemberRequest memberRequest);

        [OperationContract]
        GenericServiceResponse SaveStaff(SaveWaiterRequest request);

        [OperationContract]
        GenericServiceResponse DeleteStaff(GenericStaffRequest request);

        [OperationContract]
        GenericServiceResponse ResetPassword(GenericStaffRequest request);

        [OperationContract]
        GenericServiceResponse LoadStaffNotifications(LoadByStaffAndRestaurantRequest request);

        [OperationContract]
        GenericServiceResponse ConfirmStaffReceivedNotifications(ConfirmNotificationReceivedRequest request);

        [OperationContract]
        GenericServiceResponse SaveUnavailability(SaveUnavailabilityRequest request);
        
        [OperationContract]
        GenericServiceResponse DeleteUnavailability(DeleteUnavailabilityRequest request);

        [OperationContract]
        GenericServiceResponse UpdateLastActiveDate(UpdateLastActiveDateRequest request);
    }
}