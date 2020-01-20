using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class GenericStaffRequest
    {
        [DataMember]
        public int StaffId { get; set; }
    }
}