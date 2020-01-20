using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveShiftRequest
    {
        [DataMember]
        public ShiftDto Shift { get; set; }
    }
}