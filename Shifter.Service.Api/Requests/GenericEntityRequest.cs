using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class GenericEntityRequest 
    {
        [DataMember(IsRequired = true)]
        public int EntityId { get; set; }

        public GenericEntityRequest(int id)
        {
            EntityId = id;
        }
    }
}