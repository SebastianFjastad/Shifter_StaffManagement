using System;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadAvailableShiftRequest
    {
        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}