using TheCocktail.Application.DTOs.CocktailDBAPI;

namespace TheCocktail.Application.DTOs.Cocktails
{
    public class CocktailDetailDTO
    {
        public CocktailDetailDbDTO? CocktailDetail { get; set; }
        public CocktailFavoriteInformationDTO FavoriteInformation { get; set; }
    }

    public class CocktailFavoriteInformationDTO
    {
        public int? FavoriteId { get; set; }
        public bool IsFavorite { get; set; }
    }
}
