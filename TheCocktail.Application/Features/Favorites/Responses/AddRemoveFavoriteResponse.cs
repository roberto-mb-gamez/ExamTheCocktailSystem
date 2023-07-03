using TheCocktail.Common.Responses;

namespace TheCocktail.Application.Features.Favorites.Responses
{
    public class AddRemoveFavoriteResponse : BaseCommandResponse
    {
        public bool IsAdded { get; set; }
        public int Total { get; set; }
    }
}
