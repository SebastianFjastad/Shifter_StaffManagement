using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadByStaffAndRestaurantRequest 
    {
        [DataMember(IsRequired = true)]
        public int StaffId { get; set; }

        [DataMember(IsRequired = true)]
        public int RestaurantId { get; set; }

        public LoadByStaffAndRestaurantRequest(int staffId, int restaurantId)
        {
            StaffId = staffId;
            RestaurantId = restaurantId;
        }
    }
}