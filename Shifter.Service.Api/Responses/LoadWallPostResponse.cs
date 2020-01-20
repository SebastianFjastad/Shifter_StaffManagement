using Shifter.Service.Api.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadWallPostResponse : GenericServiceResponse
    {
        [DataMember]
        public WallPostDto WallPost { get; set; }

        public LoadWallPostResponse()
        {
            WallPost = new WallPostDto();
        }
    }
}
