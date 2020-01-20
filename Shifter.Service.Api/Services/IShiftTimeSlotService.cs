using System.ServiceModel;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface IShiftTimeSlotService
    {
        [OperationContract]
        LoadShiftTimeslotResponse LoadTimeSlotById(LoadShiftTimeSlotByIdRequest request);

        [OperationContract]
        LoadShiftTimeslotCollectionResponse LoadTimeSlots(LoadTimeSlotCollectionRequest request);

        [OperationContract]
        GenericServiceResponse SaveTimeslot(SaveShiftTimeSlotRequest request);

        [OperationContract]
        GenericServiceResponse DeleteTimeslot(DeleteShiftTimeSlotRequest request);
    }
}