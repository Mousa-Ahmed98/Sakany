using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Services
{
    public class PropertyFeaturesServices : IPropertyFeaturesServices
    {
        private readonly IPropertyFeaturesRepository propertyFeaturesRepository;

        public PropertyFeaturesServices(IPropertyFeaturesRepository propertyFeaturesRepository)
        {
            this.propertyFeaturesRepository = propertyFeaturesRepository;
        }
        public void Add(PropertyFeatures propertyFeatures)
        {
            propertyFeaturesRepository.Add(propertyFeatures);
        }

        public async Task<List<string>> GetAllByPropertyIdAsync(int propertyId)
        {
            List<PropertyFeatures> features =await propertyFeaturesRepository.GetAllByPropertyIdAsync(propertyId);
            List<string> result = new List<string>();
            foreach (PropertyFeatures propertyFeatures in features)
            {
                result.Add(propertyFeatures.FeaturesName);
            }
            return result;
        }

        public void Remove(int propertyId)
        {
            propertyFeaturesRepository.Remove(propertyId);
        }
    }
}
