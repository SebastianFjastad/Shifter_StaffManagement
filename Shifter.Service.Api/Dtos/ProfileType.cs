using System.ComponentModel;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public enum ProfileType
    {
        [EnumMember]
        Manager,
        [EnumMember]
        Waiter
    }
}