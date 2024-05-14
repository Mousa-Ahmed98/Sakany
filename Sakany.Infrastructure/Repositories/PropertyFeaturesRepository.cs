using Microsoft.EntityFrameworkCore;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Infrastructure.Repositories
{
    public class PropertyFeaturesRepository : IPropertyFeaturesRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PropertyFeaturesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(PropertyFeatures propertyFeatures)
        {
           dbContext.Set<PropertyFeatures>().Add(propertyFeatures);
            dbContext.SaveChanges();
        }

        public async Task<List<PropertyFeatures>> GetAllByPropertyIdAsync(int propertyId)
        {
            var listFeatures = await dbContext.Set<PropertyFeatures>()
                .Where(e => e.PropertyId == propertyId)
                .ToListAsync();
            return listFeatures;
        }

        public async void Remove(int propertyId)
        {
            List<PropertyFeatures> listFeatures = await GetAllByPropertyIdAsync(propertyId);
            dbContext.Set<PropertyFeatures>().RemoveRange(listFeatures);
        }
    }
}
