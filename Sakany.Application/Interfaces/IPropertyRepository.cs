using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IPropertyRepository
    {
        public Task<Properties> AddAsync(Properties property);
        public void Update(Properties property);
        public bool Delete(Properties property);
        public Task<Properties> GetByIdAsync(int propertyID);
        public Task<List<Properties>> GetAllAsync();
    }
}
