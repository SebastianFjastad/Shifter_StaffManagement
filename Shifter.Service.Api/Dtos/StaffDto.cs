using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Framework;

namespace Shifter.Service.Api.Dtos
{
    [DataContract]
    public class StaffDto
    {
        public StaffDto()
        {
            Shifts = new List<ShiftDto>();
            Restaurants = new List<RestaurantDto>();
            UnavailabilityRecords = new List<StaffUnavailabilityRecordDto>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }

        [DataMember]
        public int MaxNumberOfShiftsPerWeek { get; set; }

        [DataMember]
        public bool CanSwapShifts { get; set; }

        [DataMember]
        public bool CanWorkDoubles { get; set; }

        [DataMember]
        public bool IsExperienced { get; set; }
        
        [DataMember]
        public StaffTypeDto StaffType { get; set; }

        [DataMember]
        public IEnumerable<ShiftDto> Shifts { get; set; }

        [DataMember]
        public IEnumerable<StaffUnavailabilityRecordDto> UnavailabilityRecords { get; set; }

        [DataMember]
        public DateTime? DateLastActive { get; set; }
        
        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool HasNoEmailAddress { get; set; }

        public string FormattedLastActiveDate
        {
            get { return DateLastActive.IsNotNull() ? DateLastActive.Value.ToString(SharedConstants.ShortDateFormatWithDay) : string.Empty; }
        }

        [DataMember]
        public IList<RestaurantDto> Restaurants { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public bool IsUnavailableOnDate(DateTime date)
        {
            return UnavailabilityRecords.Any(u => date >= u.UnavailableFrom && date <= u.UnavailableTo);
        }

        public virtual bool IsTransient()
        {
            return Id <= 0;
        }
    }
}