using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IPropertyFeaturesServices
    {
        public void Add(PropertyFeatures propertyFeatures);
        public void Remove(int propertyId);
        public Task<List<string>> GetAllByPropertyIdAsync(int propertyId);
    }
}
