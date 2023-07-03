using Microsoft.Extensions.DependencyInjection;
using System;
using TheCocktail.Application.Contracts.ExternalServices;
using TheCocktail.ExternalServices.CocktailDBAPI.Services;

namespace TheCocktail.ExternalServices
{
    public static class ExternalServicesRegistration
    {
        public static IServiceCollection ConfigureExternalServicesRegistration(this IServiceCollection services)
        {
            services.AddHttpClient<ICocktailDBService, CocktailDBService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            services.AddScoped<ICocktailDBService, CocktailDBService>();

            // TODO: Proceso automático mediante Reflection

            return services;
        }
    }
}
