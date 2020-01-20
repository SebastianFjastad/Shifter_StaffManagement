using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SendFeedbackRequest
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public int UserAccountId { get; set; }
    }
}