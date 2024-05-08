using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Infrastructure.Repositories
{
    public class GovernorateRepository: IGovernorateRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GovernorateRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Governorate> GetAll()
        {
            return dbContext.Set<Governorate>().ToList();
        }

        public Governorate GetById(int governorateId)
        {
            Governorate? governorate= dbContext.Set<Governorate>().FirstOrDefault(g => g.GovernorateID == governorateId);
            return governorate;
        }
    }
}
