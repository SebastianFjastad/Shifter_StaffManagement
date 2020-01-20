using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class AuthenticationResponse : GenericServiceResponse
    {
        [DataMember]
        public int? UserAccountId { get; set; }

        [DataMember]
        public IEnumerable<ProfileSummary> Profiles { get; set; } 
    }
}