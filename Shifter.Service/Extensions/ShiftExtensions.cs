using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class ShiftExtensions
    {
        public static LoadShiftCollectionResponse AsLoadShiftCollectionResponse(this IEnumerable<Shift> shifts)
        {
            Guard.InstanceNotNull(shifts, "shifts");

            var result = new LoadShiftCollectionResponse();

            result.Shifts = shifts.Select(s => s.AsShiftDto());

            return result;
        }

        public static LoadShiftResponse AsLoadShiftResponse(this Shift shift)
        {
            Guard.InstanceNotNull(shift, "shift");

            var result = new LoadShiftResponse();
            
            result.Shift = shift.AsShiftDto();

            return result;
        }

        public static ShiftDto AsShiftDto(this Shift shift)
        {
            Guard.InstanceNotNull(shift, "shift");

            var shiftDto = MappingUtility.Map<Shift, ShiftDto>(shift);

            if (shift.Staff.IsNotNull())
            {
                shiftDto.Staff = shift.Staff.AsStaffDto();
            }

            return shiftDto;
        }
        
        public static Shift AsDomainModel(this ShiftDto shiftDto)
        {
            Guard.InstanceNotNull(shiftDto, "shiftDto");

            var shift = MappingUtility.Map<ShiftDto, Shift>(shiftDto);

            return shift;
        }
    }
}