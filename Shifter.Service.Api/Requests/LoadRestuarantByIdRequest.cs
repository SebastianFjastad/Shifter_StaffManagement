using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadRestuarantByIdRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }
    }
}