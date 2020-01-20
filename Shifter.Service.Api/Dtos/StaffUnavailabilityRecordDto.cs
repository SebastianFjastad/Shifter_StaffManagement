using System;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public class StaffUnavailabilityRecordDto
    {
        [DataMember]
        public int? Id { get; set; }

        /// <summary>
        /// First day that they are unavailable from
        /// </summary>
        [DataMember]
        public DateTime UnavailableFrom { get; set; }

        /// <summary>
        /// Last day that they are unavailable before returning
        /// </summary>
        [DataMember]
        public DateTime UnavailableTo { get; set; }
    }
}