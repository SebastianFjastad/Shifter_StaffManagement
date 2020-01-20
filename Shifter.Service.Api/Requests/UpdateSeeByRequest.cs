using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class UpdateSeeByRequest
    {

        [DataMember]
        public int StaffId { get; set; }

        [DataMember]
        public int RestaurantId { get; set; }
    }
}