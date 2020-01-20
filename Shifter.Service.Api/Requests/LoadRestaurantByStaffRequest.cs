using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadRestaurantByStaffRequest
    {
        [DataMember]
        public int StaffId { get; set; }
    }
}