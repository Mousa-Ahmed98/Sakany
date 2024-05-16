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
    public class ImageServices : IImageServices
    {
        private readonly IImageRepository imageRepository;
        public ImageServices(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public void Add(PropertyImage propertyImage)
        {
            imageRepository.Add(propertyImage);
        }
        public async Task<List<PropertyImage>> GetByPropertyIdAsync(int propertyId)
        {
            return await imageRepository.GetByPropertyIdAsync(propertyId);
        }


        //not implemented 
        public void Delete(int propertyId)
        {
            throw new NotImplementedException();
        }

        public void Update(PropertyImage propertyImage)
        {
            throw new NotImplementedException();
        }
    }

}
