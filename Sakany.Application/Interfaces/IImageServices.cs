using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IImageServices
    {
        public void Add(PropertyImage propertyImage);
        public void Update(PropertyImage propertyImage);
        public void Delete(int propertyId);
        public Task<List<PropertyImage>> GetByPropertyIdAsync(int propertyId);

    }
}
