using System;
using System.Linq;
using System.Threading.Tasks;
using TheCocktail.Application.Contracts.ExternalServices;
using TheCocktail.Application.DTOs.CocktailDBAPI;
using TheCocktail.Application.DTOs.Cocktails;
using TheCocktail.Application.Features.Favorites;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Application.Features.Cocktails
{
    public interface ICocktailService
    {
        Task<CocktailDbDTO> GetCocktailsByNameOrIngredient(SearchCocktailsDTO searchCocktails);
        Task<CocktailDetailDTO> GetCocktailDetail(int userId, string cocktailId);
    }

    public class CocktailService : ICocktailService
    {
        private readonly ICocktailDBService _cocktailDBService;
        private readonly IFavoriteService _favoriteService;

        public CocktailService(ICocktailDBService cocktailDBService,
            IFavoriteService favoriteService)
        {
            _cocktailDBService = cocktailDBService ?? throw new ArgumentNullException(nameof(cocktailDBService));
            _favoriteService = favoriteService ?? throw new ArgumentNullException(nameof(favoriteService));
        }

        public async Task<CocktailDbDTO> GetCocktailsByNameOrIngredient(SearchCocktailsDTO searchCocktails)
        {
            CocktailDbDTO cocktails = new CocktailDbDTO();

            if (searchCocktails.TipoBusqueda == TipoBusqueda.Nombre)
                cocktails = await _cocktailDBService.GetCocktailsByName(searchCocktails.Search);
            else
                cocktails = await _cocktailDBService.GetCocktailsByIngredient(searchCocktails.Search);

            cocktails.drinks = cocktails.drinks.OrderBy(d => d.strDrink);

            return cocktails;
        }

        public async Task<CocktailDetailDTO> GetCocktailDetail(int userId, string cocktailId)
        {
            Favorite? favoriteInfo = null;
            var cocktailDetail = await _cocktailDBService.GetCocktailById(cocktailId);

            if (cocktailDetail != null)
            {
                favoriteInfo = await _favoriteService.GetFavoriteByUserAndDrunk(userId, cocktailId);
            }

            return new CocktailDetailDTO
            {
                CocktailDetail = cocktailDetail,
                FavoriteInformation = new CocktailFavoriteInformationDTO
                {
                    IsFavorite = favoriteInfo != null,
                    FavoriteId = favoriteInfo?.FavoriteId
                }
            };
        }
    }
}
