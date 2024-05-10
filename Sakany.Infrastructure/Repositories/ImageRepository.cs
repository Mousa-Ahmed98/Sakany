using Microsoft.EntityFrameworkCore;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ImageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(PropertyImage propertyImage)
        {
            dbContext.Set<PropertyImage>()
                .Add(propertyImage);
            dbContext.SaveChanges();
        }

        public void Delete(PropertyImage propertyImage)
        {
            dbContext.Set<PropertyImage>()
                .Remove(propertyImage);
            dbContext.SaveChanges();
        }

     

        public async Task<List<PropertyImage>> GetByPropertyIdAsync(int propertyId)
        {
            return await dbContext.Set<PropertyImage>()
                .Where(i=>i.PropertyId== propertyId)
                .ToListAsync();
        }

        //not implemented 
        public void Update(PropertyImage propertyImage)
        {
            throw new NotImplementedException();
        }
        public void Delete(int propertyId)
        {
            throw new NotImplementedException();
        }


    }
}
