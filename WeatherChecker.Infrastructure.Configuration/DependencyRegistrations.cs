using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherChecker.Application.Interfaces.DataProviders;

namespace WeatherChecker.Infrastructure.Configuration
{
    public static class DependencyRegistrations
    {
        public static void AddConfigurationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CitiesOptions>(configuration.GetSection(CitiesOptions.SectionName));
            services.AddTransient<ICityDataProvider, CityDataProvider>();
        }
    }
}
