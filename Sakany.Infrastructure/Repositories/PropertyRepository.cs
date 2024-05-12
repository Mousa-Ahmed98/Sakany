using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sakany.Application.DTOS;
using Sakany.Application.Helper;
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

        public List<displayPropertyDTO> GetAllProperties(int pageNum, int pageSize, int numOfRooms, string priceRange, int govId, int city)
        {
            //Pagination 
            int total = dbContext.Properties.Count();
            var PaginatedList = new PaginatedList(total, pageNum, pageSize);
            int skiped_data = (pageNum - 1) * pageSize;
            int cityid = Convert.ToInt32(city);
            //Search 
            var query = dbContext.Properties.AsQueryable();

            if (numOfRooms > 0)
            {
                query = query.Where(p => p.RoomsNumber == numOfRooms);
            }

            if (!string.IsNullOrEmpty(priceRange))
            {
                int minPrice;
                int maxPrice;

                if (priceRange.StartsWith("<"))
                {
                    maxPrice = int.Parse(priceRange.Substring(1));
                    query = query.Where(p => p.Price < maxPrice);
                }
                else if (priceRange.StartsWith(">"))
                {
                    minPrice = int.Parse(priceRange.Substring(1));
                    query = query.Where(p => p.Price > minPrice);
                }
                else
                {
                    var priceRanges = priceRange.Split('-').Select(int.Parse).ToArray();
                    if (priceRanges.Length == 2)
                    {
                        minPrice = priceRanges[0];
                        maxPrice = priceRanges[1];
                        query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
                    }
                }
            }

            if (govId > 0)
            {
                query = query.Where(p => p.GovernorateID == govId);
            }

            if (city >0)
            {
                query = query.Where(p => p.City == city);
            }

            List<Properties> properties = query.Skip(skiped_data).Take(pageSize).ToList();

            List<displayPropertyDTO> displayPropertyDTOs = mapper.Map<List<displayPropertyDTO>>(properties);

            foreach (var property in displayPropertyDTOs)
            {
    
                property.Address = dbContext.Governorates.Where(g => g.GovernorateID == property.govID).Select(g => g.Name).FirstOrDefault() + ", " +
                    dbContext.Cities.Where(c => c.Id == property.cityId).Select(c => c.Name).FirstOrDefault();
                property.imageUrl = dbContext.PropertyImages.Where(img => img.PropertyId == property.id).Select(img => img.ImageUrl).FirstOrDefault();
            }
            return displayPropertyDTOs;
        }


        public List<displayPropertyDTO> GetRandomProperties(int size = 6)
        {
            string sqlQuery = $"select top {size} * from Properties order by NEWID();";
            List<Properties> properties = dbContext.Properties
                  .FromSqlRaw(sqlQuery)
                  .ToList();

            List<displayPropertyDTO> displayPropertyDTOs = mapper.Map<List<displayPropertyDTO>>(properties);
            foreach (var property in displayPropertyDTOs)
            {
              
                property.Address = dbContext.Governorates.Where(g => g.GovernorateID == property.govID).Select(g => g.Name).FirstOrDefault() + ", " +
                    dbContext.Cities.Where(c => c.Id == property.cityId).Select(c => c.Name).FirstOrDefault();
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
