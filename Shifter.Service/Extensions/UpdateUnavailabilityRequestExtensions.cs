using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Service.Api.Responses
{
    public static class UpdateUnavailabilityRequestExtensions
    {
        public static IEnumerable<StaffUnavailabilityRecord> AsStaffUnavailabilityRecords(this SaveUnavailabilityRequest unavailabilityRequest)
        {
            Guard.InstanceNotNull(unavailabilityRequest, "unavailabilityRequest");

            var unavailabilityRecords = unavailabilityRequest.Unavailability.Select(u => u.AsStaffUnavailabilityRecord(unavailabilityRequest.StaffId)).ToList();

            return unavailabilityRecords;
        }

        public static StaffUnavailabilityRecord AsStaffUnavailabilityRecord(this StaffUnavailabilityRecordDto unavailabilityDtoRecord, int staffId)
        {
            Guard.InstanceNotNull(unavailabilityDtoRecord, "unavailabilityDtoRecord");

            var unavailabilityRecord = new StaffUnavailabilityRecord()
            {
                StaffId = staffId,
                UnavailableFrom = unavailabilityDtoRecord.UnavailableFrom,
                UnavailableTo = unavailabilityDtoRecord.UnavailableTo
            };

            return unavailabilityRecord;
        }
    }
}
