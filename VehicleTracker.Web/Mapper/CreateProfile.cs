using AutoMapper;
using VehicleTracker.Core.Entities;
using VehicleTracker.Web.Dto;

namespace VehicleTracker.Web.Mapper
{
    public class CreateProfile:Profile
    {
        public CreateProfile()
        {
            CreateMap<Vehicles, VehicleRequestDto>().ReverseMap();
            CreateMap<Position, PositionRequestDto>().ReverseMap();
            CreateMap<Position, PositionResponseDto>().ForMember(r => r.Latitude ,o => o.MapFrom(s => s.Latitude)).ForMember(r => r.Longitude, s => s.MapFrom(o => o.Longitude)).ReverseMap();
            CreateMap<UserRequestDto, User>().ReverseMap();
            CreateMap<UserResponseDto, User>().ReverseMap();
            CreateMap<Vehicles, VehicleResponseDto>().ReverseMap();
        }
    }
}
