using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class LoadShiftTimeslotResponseExtensions
    {
        public static LoadShiftTimeslotResponse AsLoadShiftTimeslotResponse(this ShiftTimeslot shiftTimeslot)
        {
            Guard.InstanceNotNull(shiftTimeslot, "shiftTimeslot");

            var result = new LoadShiftTimeslotResponse();

            result.ShiftTimeSlot = MappingUtility.Map<ShiftTimeslot, ShiftTimeSlotDto>(shiftTimeslot);

            return result;
        }
    }
}