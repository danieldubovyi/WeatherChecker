using WeatherChecker.Domain.Entities;

namespace WeatherChecker.Application.Interfaces.DataProviders
{
    public interface IWeatherDataProvider
    {
        Weather GetWeatherData(decimal lat, decimal lon);
    }
}
