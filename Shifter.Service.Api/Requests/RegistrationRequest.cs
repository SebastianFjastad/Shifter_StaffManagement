using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class RegistrationRequest
    {
        [DataMember(IsRequired = true)]
        public string CompanyName { get; set; }
        
        [DataMember(IsRequired = true)]
        public string ManagerFirstName { get; set; }

        [DataMember]
        public string ManagerLastName { get; set; }

        [DataMember(IsRequired = true)]
        public string ManagerEmailAddress { get; set; }

        [DataMember]
        public string CompanyAddress { get; set; }

        [DataMember]
        public string ManagerUsername { get; set; }

        [DataMember]
        public string ManagerPassword { get; set; }
    }
}