using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveWaiterRequest
    {
        [DataMember]
        public StaffDto Staff { get; set; }

        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public bool StaffMemberHasEmailAddress { get; set; }
    }
}