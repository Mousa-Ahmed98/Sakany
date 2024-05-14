using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Services
{
    public class FavoriteServices : IFavoriteServices
    {
        private readonly IFavoriteRepository favoriteRepository;

        public FavoriteServices(IFavoriteRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }
        public void Add(Favorite favorite)
        {
            favoriteRepository.Add(favorite);
        }

        public void Delete(Favorite favorite)
        {
           favoriteRepository.Delete(favorite);
        }

        public List<FavoriteDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Favorite favorite)
        {
            favoriteRepository.Update(favorite);
        }
    }
}
