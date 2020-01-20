using System.Collections.Generic;
using Framework.Domain;

namespace Shifter.Managers.Domain.Models
{
    public class Restaurant : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual IEnumerable<Waiter> Waiters { get; set; }

        public virtual IEnumerable<Manager> Managers { get; set; }

        public virtual IEnumerable<ShiftTemplate> ShiftTemplates { get; set; }

        public virtual IEnumerable<Settings> Settings { get; set; }
    }
}
