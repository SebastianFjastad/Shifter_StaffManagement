using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class LoadRestuarantByManagerRequest
    {
        [DataMember]
        public int ManagerId { get; set; }
    }
}