using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public class ManagerDto
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }

        [DataMember]
        public IEnumerable<RestaurantDto> Restaurants { get; set; }
    }
}