using Sakany.Application.DTOS;
using Sakany.Core.Entities;

namespace Sakany.Application.Interfaces
{
    public interface IPropertyRepository
    {
        public Task<Properties> AddAsync(Properties property);
        public void Update(Properties property);
        public bool Delete(Properties property);
        public Task<Properties> GetByIdAsync(int propertyID);
        public Task<List<Properties>> GetAllAsync();
        public List<displayPropertyDTO> GetAllProperties(int pageNum, int pageSize, int numOfRooms, string priceRange, int govId, string city);
        public List<displayPropertyDTO> GetRandomProperties(int size);

    }
}
