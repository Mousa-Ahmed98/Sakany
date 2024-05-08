using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CityRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<City> GetAll(int governorateI)
        {
            return dbContext.Set<City>()
                .Where(c=>c.GovernorateID == governorateI)
                .ToList();
        }
    }
}
