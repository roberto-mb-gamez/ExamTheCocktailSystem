using Microsoft.Extensions.DependencyInjection;
using TheCocktail.Application.Features.Cocktails;
using TheCocktail.Application.Features.Favorites;

namespace TheCocktail.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            // Manualmente Service registration
            services.AddScoped<ICocktailService, CocktailService>();
            services.AddScoped<IFavoriteService, FavoriteService>();

            // TODO: Proceso automático mediante Reflection

            return services;
        }
    }
}
