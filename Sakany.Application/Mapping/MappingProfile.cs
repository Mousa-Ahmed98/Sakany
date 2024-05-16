using AutoMapper;
using Sakany.Application.DTOS;
using Sakany.Core.Entities;

namespace Sakany.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //        TSource          TDestination
            CreateMap<RegisterUserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, LoginUserDTO>();

            CreateMap<EditUserProfileDTO, ApplicationUser>();
            CreateMap<ApplicationUser, EditUserProfileDTO>();
            CreateMap<ApplicationUser, ApplicationUser>()
            .ForMember(dest => dest.SecondPhoneNumber, opt => opt.MapFrom(src => src.SecondPhoneNumber))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus))
            .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
            .ForMember(dest => dest.Employment, opt => opt.MapFrom(src => src.Employment))
            .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job));

            CreateMap<Properties, displayPropertyDTO>()
       .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
       .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.numOfRooms, opt => opt.MapFrom(src => src.RoomsNumber))
       .ForMember(dest => dest.area, opt => opt.MapFrom(src => src.Area))
       .ForMember(dest => dest.numOfBathrooms, opt => opt.MapFrom(src => src.BathroomNumber))
       .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
       .ForMember(dest => dest.govID, opt => opt.MapFrom(src => src.GovernorateID))
       .ForMember(dest => dest.cityId, opt => opt.MapFrom(src => src.City))
       .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
       .ForMember(dest => dest.ownerName, opt => opt.MapFrom(src => src.Name));

            CreateMap<PropertyImage, displayPropertyDTO>()
                .ForMember(dest => dest.imageUrl, opt => opt.MapFrom(src => src.ImageUrl));





            CreateMap<Governorate, GovernorateDTO>();
            CreateMap<GovernorateDTO, Governorate>();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

            CreateMap<PropertyDTO, Properties>();
            CreateMap<Properties, PropertyDTO>();

            CreateMap<CustomDataOfUserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, CustomDataOfUserDTO>();

        }
    }
}
