using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadSettingsByRestaurantRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }
    }
}