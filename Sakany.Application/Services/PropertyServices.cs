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
    public class PropertyServices : IPropertyServices
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IGovernorateRepository governorateRepository;
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;

        public PropertyServices(
            IPropertyRepository propertyRepository
            ,IGovernorateRepository governorateRepository
            ,IImageRepository imageRepository
            ,IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.governorateRepository = governorateRepository;
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }
        public async Task<PropertiesDetilesDTO> Add(PropertyDTO propertyDTO)
        {
            Properties property=mapper.Map<Properties>(propertyDTO);
           Properties properties =await propertyRepository.AddAsync(property);
            PropertiesDetilesDTO propertiesDetilesDTO = await MapPropertyToDTOAsync(properties);
            return propertiesDetilesDTO;
        }

       

        public async Task<List<PropertiesDetilesDTO>> GetAll()
        {
            var ListProperties=await propertyRepository.GetAllAsync();
            List<PropertiesDetilesDTO> ListPropertiesDetilesDTOs = new List<PropertiesDetilesDTO>();
            foreach (var property in ListProperties) {
                PropertiesDetilesDTO propertiesDetilesDTO = await MapPropertyToDTOAsync(property);
                ListPropertiesDetilesDTOs.Add(propertiesDetilesDTO);
            }
            return ListPropertiesDetilesDTOs;
        }
        //not implemented 
        public bool Delete(Properties property)
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

        private async Task<PropertiesDetilesDTO> MapPropertyToDTOAsync(Properties properties)
        {
            PropertiesDetilesDTO dto = new PropertiesDetilesDTO();
            if (properties != null)
            {
                dto.Id = properties.Id;
                dto.Type = properties.Type;
                dto.City = properties.City;
                dto.Title = properties.Title;
                dto.Status = properties.Status;
                dto.Area = (decimal)properties.Area;
                dto.Price = (decimal)properties.Price;
                dto.RoomsNumber = properties.RoomsNumber;
                dto.BathroomNumber = properties.BathroomNumber;
                //get name of governorate 
                Governorate governorate = governorateRepository.GetById(properties.Id);
                dto.Governorate = governorate.Name;
                //get all images 
                List<PropertyImage> propertyImages = await imageRepository.GetByPropertyIdAsync(properties.Id);
                if (propertyImages != null)
                {
                    foreach (var image in propertyImages)
                    {
                        dto.Images.Add(image.ImageUrl);
                    }
                }
            }
            return dto;
        }
    }
}
