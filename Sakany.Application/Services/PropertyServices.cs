﻿using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;

namespace Sakany.Application.Services
{
    public class PropertyServices : IPropertyServices
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IGovernorateRepository governorateRepository;
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;
        private readonly ICityServices cityServices;
        private readonly IPropertyFeaturesRepository featuresRepository;

        public PropertyServices(
            IPropertyRepository propertyRepository,
            IPropertyFeaturesRepository featuresRepository
            , IGovernorateRepository governorateRepository
            , IImageRepository imageRepository
            , IMapper mapper,
            ICityServices cityServices)

        {
            this.propertyRepository = propertyRepository;
            this.governorateRepository = governorateRepository;
            this.imageRepository = imageRepository;
            this.mapper = mapper;
            this.cityServices = cityServices;
            this.featuresRepository = featuresRepository;
        }
        public async Task<int> Add(PropertyDTO propertyDTO,string UserID)
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
                GovernorateID = propertyDTO.Governorate,
                Date=DateTime.Now,
                UserId=UserID,
            };

            Properties properties = await propertyRepository.AddAsync(property);
            return properties.Id;
        }
        public async Task<List<PropertiesDetilesDTO>> GetAll()
        {
            var ListProperties = await propertyRepository.GetAllAsync();
            List<PropertiesDetilesDTO> ListPropertiesDetilesDTOs = new List<PropertiesDetilesDTO>();
            foreach (var property in ListProperties)
            {
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

        public async Task<PropertiesDetilesDTO> GetById(int propertyID)
        {
            Console.WriteLine("//////////////////////////////");
            Console.WriteLine(propertyID);
            Properties property = await propertyRepository.GetByIdAsync(propertyID);
            if(property == null)
            {
                return null;
            }
            Console.WriteLine(property);
            PropertiesDetilesDTO propertyDetailsDto = mapper.Map<PropertiesDetilesDTO>(property);
            List<PropertyFeatures> propertyFeatures =
                await featuresRepository.GetAllByPropertyIdAsync(propertyID);
            Console.WriteLine(propertyFeatures[0].FeaturesName);
            propertyDetailsDto.Features = new List<string>();
            List<PropertyImage> propertyImages = await imageRepository.GetByPropertyIdAsync(propertyID);
            propertyDetailsDto.Images = new List<string>();
            foreach (PropertyFeatures item in propertyFeatures)
            {
                propertyDetailsDto.Features.Add(item.FeaturesName);
            }

            foreach (PropertyImage item in propertyImages)
            {
                propertyDetailsDto.Images.Add(item.ImageUrl);
            }
            return propertyDetailsDto;
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
                if (cityDTO != null)
                {
                    dto.City = cityDTO.Name;
                }
            }
            return dto;
        }

        public PropertyPaginationResponseDTO GetAllProperties(int pageNum, int pageSize, int numOfRooms, string priceRange, int govId, int cityId)
        {
            return propertyRepository.GetAllProperties(pageNum, pageSize, numOfRooms, priceRange, govId, cityId);
        }
        public List<displayPropertyDTO> GetRandomProperties(int size)
        {
            return propertyRepository.GetRandomProperties(size);
        }

        public async Task<Proposal> AddProposal(ProposalDto proposalDto)
        {
            Proposal proposal = mapper.Map<Proposal>(proposalDto);
            return await propertyRepository.AddProposalAsync(proposal);
        }

        public async Task<List<Proposal>> GetAllProposals(int Id)
        {
            return await propertyRepository.GetAllProposalsAsync(Id);
        }
    }
}
