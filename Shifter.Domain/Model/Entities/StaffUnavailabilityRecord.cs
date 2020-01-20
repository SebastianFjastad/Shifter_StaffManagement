using Framework.Domain;
using System;

namespace Shifter.Domain.Model.Entities
{
    public class StaffUnavailabilityRecord : Entity
    {
        public virtual DateTime UnavailableFrom { get; set; }

        public virtual DateTime UnavailableTo { get; set; }

        public virtual int StaffId { get; set; }
    }
}
