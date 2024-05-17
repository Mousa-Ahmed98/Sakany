using Sakany.Application.DTOS;
using Sakany.Core.Entities;

namespace Sakany.Application.Interfaces
{
    public interface IPropertyServices
    {
        public Task<int> Add(PropertyDTO property);
        public void Update(Properties property);
        public bool Delete(Properties property);
        public Task<PropertiesDetilesDTO> GetById(int propertyID);
        public Task<List<PropertiesDetilesDTO>> GetAll();

        public Task<PropertiesDetilesDTO> MapPropertyToDTOAsync(Properties properties);
        public PropertyPaginationResponseDTO GetAllProperties(int pageNum, int pageSize, int numOfRooms, string priceRange, int govId, int city);
        public List<displayPropertyDTO> GetRandomProperties(int size);
        public Task<Proposal> AddProposal(ProposalDto proposalDto);
        public Task<List<Proposal>> GetAllProposals(int Id);

    }
}
