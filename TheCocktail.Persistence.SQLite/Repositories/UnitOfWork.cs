using System;
using System.Threading.Tasks;
using TheCocktail.Application.Contracts.Persistence;

namespace TheCocktail.Persistence.SQLite.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IFavoriteRepository _favoriteRepository;
        public IFavoriteRepository FavoriteRepository
            => _favoriteRepository ??= new FavoriteRepository(_context);

        private readonly CocktailDbContext _context;

        public UnitOfWork(CocktailDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
