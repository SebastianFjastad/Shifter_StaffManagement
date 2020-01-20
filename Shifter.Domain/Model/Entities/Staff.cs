using System;
using System.Collections.Generic;
using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class Staff : Entity, IProfile
    {
        #region Constructors

        public Staff()
        {
            Restaurants = new List<Restaurant>();
        }

        #endregion

        #region Properties

        public virtual int MaxNumberOfShiftsPerWeek { get; set; }

        public virtual bool CanSwapShifts { get; set; }

        public virtual bool CanWorkDoubles { get; set; }

        public virtual bool IsExperienced { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual IList<Restaurant> Restaurants { get; set; }

        public virtual IList<StaffUnavailabilityRecord> UnavailabilityRecords { get; set; }

        public UserAccount UserAccount { get; set; }

        public StaffType StaffType { get; set; }

        public DateTime? DateLastActive { get; set; }

        public bool WelcomeEmailSent { get; set; }

        #endregion
    }
}
