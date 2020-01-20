using System.ServiceModel;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface IRestaurantService
    {
        [OperationContract]
        LoadRestaurantResponse LoadRestaurantByManager(LoadRestuarantByManagerRequest request);

        [OperationContract]
        LoadRestaurantResponse LoadRestaurantByStaff(LoadRestaurantByStaffRequest request);

        [OperationContract]
        LoadRestaurantResponse LoadRestaurantById(LoadRestuarantByIdRequest request);
    }
}