using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCocktail.Application.Contracts.Persistence;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Persistence.SQLite.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly CocktailDbContext _dbContext;

        public FavoriteRepository(CocktailDbContext cocktailDbContext)
            : base(cocktailDbContext)
        {
            _dbContext = cocktailDbContext;
        }

        public async Task<List<Favorite>> GetFavoritesByUserId(int userId)
        {
            return await _dbContext.Favorites.Where(f => f.UserId == userId).ToListAsync();
        }

        public async Task<Favorite?> GetFavoriteByUserIdAndDrinkId(int userId, string drinkId)
        {
            return await _dbContext
                .Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.DrinkId == drinkId);
        }
    }
}
