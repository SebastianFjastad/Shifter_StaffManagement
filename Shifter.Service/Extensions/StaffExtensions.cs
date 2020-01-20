using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;

namespace Shifter.Service.Api.Responses
{
    public static class StaffExtensions
    {
        public static IEnumerable<StaffDto> AsStaffDtos(this IEnumerable<Staff> staff)
        {
            Guard.InstanceNotNull(staff, "staff");

            var staffDtos = staff.Select(w => w.AsStaffDto()).ToList();

            return staffDtos;
        }

        public static IEnumerable<StaffTypeDto> AsStaffTypeDtos(this IEnumerable<StaffType> staffTypes)
        {
            Guard.InstanceNotNull(staffTypes, "staffTypes");

            var staffTypeDtos = staffTypes.Select(s => s.AsStaffTypeDto()).ToList();

            return staffTypeDtos;
        }

        public static StaffDto AsStaffDto(this Staff staff)
        {
            Guard.InstanceNotNull(staff, "staff");

            var staffDto = MappingUtility.Map<Staff, StaffDto>(staff);
            staffDto.FirstName = staff.UserAccount.FirstName;
            staffDto.LastName = staff.UserAccount.LastName;
            staffDto.EmailAddress = staff.UserAccount.EmailAddress;
            staffDto.ContactNumber = staff.UserAccount.ContactNumber;
            staffDto.HasNoEmailAddress = !staff.IsTransient() && staff.UserAccount.EmailAddress.IsNullOrEmpty();

            return staffDto;
        }

        public static StaffTypeDto AsStaffTypeDto(this StaffType staff)
        {
            Guard.InstanceNotNull(staff, "staff");

            var dto = MappingUtility.Map<StaffType, StaffTypeDto>(staff);

            return dto;
        }

        public static Staff AsDomainModel(this StaffDto staffDto, bool createUserAccountIfNull = false)
        {
            Guard.InstanceNotNull(staffDto, "staffDto");

            var staff = MappingUtility.Map<StaffDto, Staff>(staffDto);

            if (staff.UserAccount.IsNull() && createUserAccountIfNull)
            {
                staff.UserAccount = new UserAccount();
            }

            staff.UserAccount.FirstName = staffDto.FirstName;
            staff.UserAccount.LastName = staffDto.LastName;
            staff.UserAccount.EmailAddress = staffDto.EmailAddress;
            staff.UserAccount.ContactNumber = staffDto.ContactNumber;

            return staff;
        }
    }
}