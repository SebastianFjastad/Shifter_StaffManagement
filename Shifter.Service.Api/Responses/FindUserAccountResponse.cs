using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class FindUserAccountResponse : GenericServiceResponse
    {
        [DataMember]
        public int? UserAccountId { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }
    }
}