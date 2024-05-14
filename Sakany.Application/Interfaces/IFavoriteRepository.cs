using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IFavoriteRepository
    {
        public void Add(Favorite favorite);
        public void Update(Favorite favorite);
        public void Delete(Favorite favorite);
        public List<Favorite> GetAll();
    }
}
