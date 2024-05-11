using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;

namespace Sakany.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public PropertyRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Properties> AddAsync(Properties property)
        {
            await dbContext.Set<Properties>().AddAsync(property);
            dbContext.SaveChanges();
            return property;
        }

        public bool Delete(Properties property)
        {
            try
            {
                dbContext.Set<Properties>().Remove(property);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Properties>> GetAllAsync()
        {
            return await dbContext.Set<Properties>()
                .ToListAsync();
        }

        public List<displayPropertyDTO> GetAllProperties()
        {
            List<Properties> properties = dbContext.Properties.ToList();
            List<displayPropertyDTO> displayPropertyDTOs = mapper.Map<List<displayPropertyDTO>>(properties);

            foreach (var property in displayPropertyDTOs)
            {
                int cityId = int.Parse(property.cityId);
                property.Address = dbContext.Governorates.Where(g => g.GovernorateID == property.govID).Select(g => g.Name).FirstOrDefault() + ", " +
                    dbContext.Cities.Where(c => c.Id == cityId).Select(c => c.Name).FirstOrDefault();
                property.imageUrl = dbContext.PropertyImages.Where(img => img.PropertyId == property.id).Select(img => img.ImageUrl).FirstOrDefault();
            }
            return displayPropertyDTOs;
        }


        public async Task<Properties> GetByIdAsync(int propertyID)
        {
            Properties? property = await dbContext.Set<Properties>()
                .FirstOrDefaultAsync(p => p.Id == propertyID);

            return property;
        }

        public void Update(Properties property)
        {
            dbContext.Set<Properties>().Update(property);
            dbContext.SaveChanges();

        }
    }
}
