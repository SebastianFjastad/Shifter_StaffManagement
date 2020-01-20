using System.Collections;
using System.ServiceModel;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface IShiftService
    {
        [OperationContract]
        LoadShiftCollectionResponse LoadShifts(LoadShiftsRequest request);

        //[OperationContract]
        //LoadShiftCollectionResponse LoadShiftsByWaiter(LoadShiftByWaiterRequest request);

        //[OperationContract]
        //LoadShiftCollectionResponse LoadAvailableShifts(LoadAvailableShiftRequest request);

        [OperationContract]
        LoadShiftResponse LoadShift(GenericEntityRequest request);

        [OperationContract]
        GenericServiceResponse SaveShift(SaveShiftRequest request);

        [OperationContract]
        GenericServiceResponse DeleteShift(DeleteShiftRequest request);

        [OperationContract]
        GenericServiceResponse AssignShift(AssignShiftRequest request);

        [OperationContract]
        GenericServiceResponse UpdateShiftAvailablity(UpdateShiftAvailabilityRequest request);

        [OperationContract]
        CopyShiftsResponse CopyShifts(CopyShiftsRequest request);

        [OperationContract]
        GenericServiceResponse SaveShifts(ShiftsRequest request);

        [OperationContract]
        GenericServiceResponse DeleteShifts(ShiftsRequest request);
    }
}