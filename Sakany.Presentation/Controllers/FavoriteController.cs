using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.Interfaces;

namespace Sakany.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteServices favoriteServices;

        public FavoriteController(IFavoriteServices favoriteServices)
        {
            this.favoriteServices = favoriteServices;
        }

    }
}
