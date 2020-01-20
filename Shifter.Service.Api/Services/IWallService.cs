using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using System.ServiceModel;

namespace Shifter.Service.Api.Services
{
    [ServiceContract(Namespace = "http://Shifter.Service")]
    public interface IWallService
    {
        [OperationContract]
        WallPostCollectionResponse LoadWallPosts(LoadWallPostsRequest request);

        [OperationContract]
        LoadWallPostResponse LoadWallPost(GenericEntityRequest request);

        [OperationContract]
        GenericServiceResponse SaveWallPost(SaveWallPostRequest request);

        [OperationContract]
        GenericServiceResponse DeleteWallPost(GenericEntityRequest request);

        [OperationContract]
        GenericServiceResponse SaveComment(SaveCommentRequest request);

        [OperationContract]
        GenericServiceResponse UpdateSeenBy(UpdateSeeByRequest request);
    }
}
