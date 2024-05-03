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
        }
    }
}
