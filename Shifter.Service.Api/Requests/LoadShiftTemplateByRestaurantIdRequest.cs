using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadShiftTemplateByRestaurantIdRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }
    }
}