using Sakany.Application.DTOS;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
