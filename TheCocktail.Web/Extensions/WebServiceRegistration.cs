using TheCocktail.Common.Options;

namespace TheCocktail.Web.Extensions
{
    public static class WebServiceRegistration
    {
        public static IServiceCollection ConfigureWebServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));

            return services;
        }
    }
}
