using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext dbContext;

        public FavoriteRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        public void Delete(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        public List<Favorite> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Favorite favorite)
        {
            throw new NotImplementedException();
        }
    }
}
