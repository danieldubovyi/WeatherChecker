using Microsoft.Extensions.DependencyInjection;
using WeatherChecker.Application.Interfaces.Services;
using WeatherChecker.Application.Services;

namespace WeatherChecker.Application
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>();
            return services;
        }
    }
}
