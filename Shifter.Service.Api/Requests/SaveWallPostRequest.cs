using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveWallPostRequest
    {
        [DataMember]
        public WallPostDto WallPost { get; set; }

        [DataMember]
        public int PostedById { get; set; }
    }
}
