using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IImageRepository
    {
        public void Add(PropertyImage propertyImage);
        public Task<List<PropertyImage>> GetByPropertyIdAsync(int propertyId);
        public void Update(PropertyImage propertyImage);
        public void Delete(int propertyId);

    }
}
