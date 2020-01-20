using System.ServiceModel;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface ISettingsService
    {
        [OperationContract]
        LoadSettingsResponse LoadSettingsByRestaurant(LoadSettingsByRestaurantRequest request);

        [OperationContract]
        GenericServiceResponse SaveSettings(SaveSettingsRequest request);

        [OperationContract]
        GenericServiceResponse LoadRestaurantNotifications(GenericEntityRequest request);
    }
}