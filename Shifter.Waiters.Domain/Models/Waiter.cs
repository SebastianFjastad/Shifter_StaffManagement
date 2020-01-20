using System.Collections.Generic;
using Framework.Domain;

namespace Shifter.Waiters.Domain.Models
{
    public class Waiter : Entity
    {
        public string UserAuthenticationReference { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string EmailAddress { get; set; }

        public UserAccount UserAccount { get; set; }

        public string ContactNumber { get; set; }

        public virtual int MaxNumberOfShiftsPerWeek { get; set; }

        public virtual bool CanSwapShifts { get; set; }

        public virtual bool CanWorkDoubles { get; set; }

        public ICollection<Shift> Shifts { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
