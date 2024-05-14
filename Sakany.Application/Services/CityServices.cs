using AutoMapper;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Services
{
    public class CityServices : ICityServices
    {
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;

        public CityServices(
            ICityRepository cityRepository,
            IMapper mapper
            )
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }
        public List<CityDTO> GetAll(int governorateId)
        {
            List<City> cities =cityRepository.GetAll(governorateId);
            List<CityDTO> cityDTOs = mapper.Map<List<City>, List<CityDTO>>(cities);
            return cityDTOs;
        }

        public CityDTO GetById(int Id)
        {
            City city= cityRepository.GetById(Id);
            CityDTO cityDTO = mapper.Map<City, CityDTO>(city);
            return cityDTO;

        }
    }
}
