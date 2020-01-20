using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadWallPostsRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public DateTime? IncludePostsFrom { get; set; }

        [DataMember]
        public DateTime? IncludePostsTo { get; set; }

        [DataMember]
        public IEnumerable<int> StaffTypeIds { get; set; }

        [DataMember]
        public bool IncludeGroupPosts { get; set; }
    }
}
