using System.ServiceModel;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface IManagerService
    {
        [OperationContract]
        LoadManagerResponse LoadManager(GenericEntityRequest request);

        [OperationContract]
        GenericServiceResponse SaveManager(SaveManagerRequest request);
    }
}
