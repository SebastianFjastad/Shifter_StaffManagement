using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public class StaffHours
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int NoOfShifts { get; set; }

        [DataMember]
        public TimeSpan HoursWorked { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}