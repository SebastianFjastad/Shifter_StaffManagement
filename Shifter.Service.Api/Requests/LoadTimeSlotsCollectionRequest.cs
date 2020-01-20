using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadTimeSlotCollectionRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public int? StaffTypeId { get; set; }
    }
}