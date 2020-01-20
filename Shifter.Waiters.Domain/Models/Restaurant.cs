using System.Collections.Generic;
using Framework.Domain;

namespace Shifter.Waiters.Domain.Models
{
    public class Restaurant : Entity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Waiter> Waiters { get; set; }
    }
}
