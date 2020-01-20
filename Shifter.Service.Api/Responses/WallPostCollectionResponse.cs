using Shifter.Service.Api.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class WallPostCollectionResponse : GenericServiceResponse
    {
        [DataMember]
        public IEnumerable<WallPostDto> WallPosts { get; set; }

        public WallPostCollectionResponse ()
        {
            WallPosts = new List<WallPostDto>();
        }
    }
}
