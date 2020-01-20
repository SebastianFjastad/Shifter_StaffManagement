using System.Collections.Generic;
using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class Restaurant : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual IEnumerable<Staff> Staff { get; set; }

        public virtual IEnumerable<Manager> Managers { get; set; }

        public virtual IEnumerable<ShiftTemplate> ShiftTemplates { get; set; }

        public virtual IEnumerable<Settings> Settings { get; set; }
    }
}
