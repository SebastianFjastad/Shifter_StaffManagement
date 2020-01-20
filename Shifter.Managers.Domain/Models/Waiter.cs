using System.Collections.Generic;
using Framework.Domain;

namespace Shifter.Managers.Domain.Models
{
    public class Waiter : Entity
    {
        #region Constructors

        public Waiter()
        {
            Restaurants = new List<Restaurant>();
        }

        #endregion

        #region Properties

        public virtual UserAccount UserAccount { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string ContactNumber { get; set; }

        public virtual int MaxNumberOfShiftsPerWeek { get; set; }

        public virtual bool CanSwapShifts { get; set; }

        public virtual bool CanWorkDoubles { get; set; }

        public virtual bool IsExperienced { get; set; }

        public virtual IEnumerable<Shift> Shifts { get; set; }

        public virtual IList<Restaurant> Restaurants { get; set; }

        #endregion
    }
}
