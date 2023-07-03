using System.Collections.Generic;
using System.Threading.Tasks;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Application.Contracts.Persistence
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<List<Favorite>> GetFavoritesByUserId(int userId);
        Task<Favorite?> GetFavoriteByUserIdAndDrinkId(int userId, string drinkId);
    }
}
