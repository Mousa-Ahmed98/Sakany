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
        private readonly ICityServices cityServices;

        public PropertyServices(
            IPropertyRepository propertyRepository
            ,IGovernorateRepository governorateRepository
            ,IImageRepository imageRepository
            ,IMapper mapper,
            ICityServices cityServices)
        {
            this.propertyRepository = propertyRepository;
            this.governorateRepository = governorateRepository;
            this.imageRepository = imageRepository;
            this.mapper = mapper;
            this.cityServices = cityServices;
        }
        public async Task<int> Add(PropertyDTO propertyDTO)
        {
            //Properties property = mapper.Map<Properties>(propertyDTO);
            Properties property = new Properties
            {
                Title = propertyDTO.Title,
                Description = propertyDTO.Description,
                Status = propertyDTO.Status,
                Type = propertyDTO.Type,
                RoomsNumber = propertyDTO.RoomsNumber,
                Price = (float)propertyDTO.Price,
                Area = (float)propertyDTO.Area,
                BathroomNumber = propertyDTO.BathroomNumber,
                Age = Convert.ToSingle(propertyDTO.Age),
                City = propertyDTO.City,
                Name = propertyDTO.Name,
                UserName = propertyDTO.UserName,
                Phone = propertyDTO.Phone,
                Email = propertyDTO.Email,
                GovernorateID =propertyDTO.Governorate
            };

            Properties properties =await propertyRepository.AddAsync(property);
            return properties.Id;
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

        public async Task<PropertiesDetilesDTO> MapPropertyToDTOAsync(Properties properties)
        {
            PropertiesDetilesDTO dto = new PropertiesDetilesDTO();
            if (properties != null)
            {
                dto.Id = properties.Id;
                dto.Type = properties.Type;
                dto.Title = properties.Title;
                dto.Status = properties.Status;
                dto.Area = (decimal)properties.Area;
                dto.Price = (decimal)properties.Price;
                dto.RoomsNumber = properties.RoomsNumber;
                dto.BathroomNumber = properties.BathroomNumber;
                //get name of governorate 
                Governorate governorate = governorateRepository.GetById(properties.GovernorateID);
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
                CityDTO cityDTO = cityServices.GetById(properties.City);
                if(cityDTO != null)
                {
                    dto.City = cityDTO.Name;
                }
            }
            return dto;
        }
    }
}
