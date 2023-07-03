using Microsoft.EntityFrameworkCore;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Persistence.SQLite
{
    public class CocktailDbContext : DbContext
    {
        public CocktailDbContext(DbContextOptions<CocktailDbContext> options)
            : base(options)
        {
        }

        public DbSet<Favorite> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CocktailDbContext).Assembly);
        }
    }
}
