using System.Collections.Generic;
using FluentNHibernate.Data;
using Entity = Framework.Domain.Entity;

namespace Shifter.Managers.Domain.Models
{
    public class Manager : Entity
    {
        public virtual UserAccount UserAccount { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string ContactNumber { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
