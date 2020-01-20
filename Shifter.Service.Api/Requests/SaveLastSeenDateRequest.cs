using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveLastSeenDateRequest
    {
        [DataMember]
        public DateTime LastSeenDate { get; set; }

        [DataMember]
        public int StaffId { get; set; }
    }
}
