using AutoMapper;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Utilities
{
    public static class MappingUtility
    {
        static MappingUtility()
        {
            //TODO: Extract IObjectMapper Utility.
            Mapper.CreateMap<Manager, ManagerDto>();
            Mapper.CreateMap<ManagerDto, Manager>();

            Mapper.CreateMap<Restaurant, RestaurantDto>();
            Mapper.CreateMap<RestaurantDto, Restaurant>();

            Mapper.CreateMap<Settings, SettingsDto>();
            Mapper.CreateMap<SettingsDto, Settings>();

            Mapper.CreateMap<Staff, StaffDto>();
            Mapper.CreateMap<StaffDto, Staff>();

            Mapper.CreateMap<Shift, ShiftDto>();
            Mapper.CreateMap<ShiftDto, Shift>();

            Mapper.CreateMap<ShiftTemplate, ShiftTemplateDto>();
            Mapper.CreateMap<ShiftTemplateDto, ShiftTemplate>();

            Mapper.CreateMap<ShiftTimeslot, ShiftTimeSlotDto>();
            Mapper.CreateMap<ShiftTimeSlotDto, ShiftTimeslot>();

            Mapper.CreateMap<WallPost, WallPostDto>();
            Mapper.CreateMap<WallPostDto, WallPost>();

            Mapper.CreateMap<Comment, CommentDto>();
            Mapper.CreateMap<CommentDto, Comment>();

            Mapper.CreateMap<StaffType, StaffTypeDto>();
            Mapper.CreateMap<StaffTypeDto, StaffType>();

            Mapper.CreateMap<StaffUnavailabilityRecord, StaffUnavailabilityRecordDto>();
            Mapper.CreateMap<StaffUnavailabilityRecordDto, StaffUnavailabilityRecord>();
        }

        internal static TOut Map<TIn, TOut>(TIn source)
        {
            return Mapper.Map<TIn, TOut>(source);
        }
    }
}
