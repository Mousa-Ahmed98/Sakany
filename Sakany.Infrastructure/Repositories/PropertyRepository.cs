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

        public PropertyPaginationResponseDTO GetAllProperties(int pageNum, int pageSize, int numOfRooms, string priceRange, int govId, int city)
        {
            //Pagination 


            //Search 
            var query = dbContext.Properties.AsQueryable();

            if (numOfRooms > 0)
            {
                query = query.Where(p => p.RoomsNumber == numOfRooms);
            }

            if (priceRange != "all")
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

            if (city > 0)
            {
                query = query.Where(p => p.City == city);
            }


            int total = query.Count();
            var paginatedList = new PaginatedList(total, pageNum, pageSize);

            int totalPages = paginatedList.TotalPages;
            int currentPage = paginatedList.CurrentPage;
            int startPage = paginatedList.StartPage;
            int endPage = paginatedList.EndPage;

            int skipedData = (pageNum - 1) * pageSize;
            int cityId = Convert.ToInt32(city);
            List<Properties> properties = query.Skip(skipedData).Take(pageSize).ToList();
            List<displayPropertyDTO> displayPropertyDTOs = mapper.Map<List<displayPropertyDTO>>(properties);

            foreach (var property in displayPropertyDTOs)
            {
                property.isForSale = property.status == "Sale";
                property.Address = dbContext.Governorates.Where(g => g.GovernorateID == property.govID)
                                                         .Select(g => g.Name)
                                                         .FirstOrDefault() + ", " +
                                  dbContext.Cities.Where(c => c.Id == property.cityId)
                                                  .Select(c => c.Name)
                                                  .FirstOrDefault();
                property.imageUrl = dbContext.PropertyImages.Where(img => img.PropertyId == property.id)
                                                             .Select(img => img.ImageUrl)
                                                             .FirstOrDefault();
            }

            var responseDTO = new PropertyPaginationResponseDTO
            {
                Properties = displayPropertyDTOs,
                PaginationInfo = new PaginationInfoDTO
                {
                    Total = total,
                    TotalPages = totalPages,
                    CurrentPage = currentPage,
                    StartPage = startPage,
                    EndPage = endPage
                }
            };

            return responseDTO;
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
                property.isForSale = property.status == "Sale" ? true : false;

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

        public async Task<Proposal> AddProposalAsync(Proposal proposal)
        {
            await dbContext.Proposals.AddAsync(proposal);
            await dbContext.SaveChangesAsync();
            return proposal;
        }
    }
}
