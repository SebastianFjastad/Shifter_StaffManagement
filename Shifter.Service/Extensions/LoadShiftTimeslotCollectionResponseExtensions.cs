using System.Collections.Generic;
using System.Linq;
using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class LoadShiftTimeslotCollectionResponseExtensions
    {
        public static LoadShiftTimeslotCollectionResponse AsLoadShiftTimeslotCollectionResponse(this IEnumerable<ShiftTimeslot> shiftTimeslots)
        {
            Guard.InstanceNotNull(shiftTimeslots, "shiftTimeslots");

            var result = new LoadShiftTimeslotCollectionResponse();

            result.ShiftTimeslots = shiftTimeslots.Select(MappingUtility.Map<ShiftTimeslot, ShiftTimeSlotDto>).ToList();

            return result;
        }
    }
}