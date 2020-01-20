using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class CopyShiftsResponse : GenericServiceResponse
    {
        [DataMember]
        public bool HasConflictWarning { get; set; }
    }
}