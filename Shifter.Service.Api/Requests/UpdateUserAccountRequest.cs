using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class UpdateUserAccountRequest
    {
        [DataMember]
        public int UserAccountId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }
    }
}