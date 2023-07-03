using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCocktail.Application.Contracts.Persistence;
using TheCocktail.Persistence.SQLite.Repositories;

namespace TheCocktail.Persistence.SQLite
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CocktailDbContext>(options 
                    => options.UseSqlite(configuration.GetConnectionString("CocktailDb")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            // TODO: Proceso automático mediante Reflection

            return services;
        }
    }
}
