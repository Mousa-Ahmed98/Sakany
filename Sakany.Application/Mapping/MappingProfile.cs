using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


            CreateMap<Governorate, GovernorateDTO>(); // Mapping between Governorate and GovernorateDTO
            CreateMap<GovernorateDTO, Governorate>(); // Mapping between GovernorateDTO and Governorate

            CreateMap<City, CityDTO>(); // Mapping between City and CityDTO
            CreateMap<CityDTO, City>(); // Mapping between CityDTO and City
        }
    }
}
