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
    public class PropertyServices : IPropertyServices
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IGovernorateRepository governorateRepository;

        public PropertyServices(
            IPropertyRepository propertyRepository
            ,IGovernorateRepository governorateRepository)
        {
            this.propertyRepository = propertyRepository;
            this.governorateRepository = governorateRepository;
        }
        public async Task<PropertiesDetilesDTO> Add(Properties property)
        {
           Properties properties =await propertyRepository.Add(property);
            PropertiesDetilesDTO dto = new PropertiesDetilesDTO();
            if(properties != null)
            {
                dto.Area = (decimal)properties.Area;
                dto.City = properties.City;
                dto.Status = properties.Status;
                dto.RoomsNumber = properties.RoomsNumber;
                dto.BathroomNumber = properties.BathroomNumber;
                dto.Id = properties.Id;
                Governorate governorate= governorateRepository.GetById(properties.Id);
                dto.Governorate = governorate.Name;
                //get all images 

            }
            return dto;
        }

        public bool Delete(Properties property)
        {
            throw new NotImplementedException();
        }

        public Task<List<PropertiesDetilesDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PropertiesDetilesDTO> GetById(int propertyID)
        {
            throw new NotImplementedException();
        }

        public void Update(Properties property)
        {
            throw new NotImplementedException();
        }
    }
}
