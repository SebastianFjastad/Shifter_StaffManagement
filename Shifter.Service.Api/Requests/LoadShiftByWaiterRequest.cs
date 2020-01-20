using System;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadShiftByWaiterRequest
    {
        [DataMember] 
        public int RestaurantId { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public DateTime? ToDate { get; set; }

        [DataMember]
        public int WaiterId { get; set; }
    }
}