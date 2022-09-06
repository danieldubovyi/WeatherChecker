using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherChecker.Application.Interfaces.DataProviders;

namespace WeatherChecker.Infrastructure.OpenWeather
{
    public static class DependencyRegistrations
    {
        public static void AddOpenWeather(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IWeatherDataProvider, WeatherDataProvider>();
            services.Configure<OpenWeatherOptions>(configuration.GetSection(OpenWeatherOptions.SectionName));
            services.AddHttpClient<IWeatherDataProvider, WeatherDataProvider>();
        }
    }
}
