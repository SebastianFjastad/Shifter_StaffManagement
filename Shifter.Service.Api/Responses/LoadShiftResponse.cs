using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class LoadShiftResponse : GenericServiceResponse
    {
        [DataMember]
        public ShiftDto Shift { get; set; }
    }
}