using Microsoft.AspNetCore.Mvc;
using TheCocktail.Application.Features.Favorites;
using TheCocktail.Common.Responses;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Web.Controllers
{
    public class FavoriteCocktailsController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private const int USER_TEST = 1;

        public FavoriteCocktailsController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService ?? throw new ArgumentNullException(nameof(favoriteService));
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoveFavorite(Favorite favorite)
        {
            BaseCommandResponse response;

            if (favorite.FavoriteId == 0)
            {
                favorite.UserId = USER_TEST;
                response = await _favoriteService.AddFavorite(favorite);
            }
            else
            {
                response = await _favoriteService.RemoveFavorite(new Domain.Entities.Favorite
                {
                    FavoriteId = favorite.FavoriteId
                });
            }

            return new OkObjectResult(response);
        }

        public async Task<IActionResult> ListFavorites()
            => View(await _favoriteService.GetFavoritesByUserId(USER_TEST));
    }
}
