using WeatherChecker.Domain.Entities;

namespace WeatherChecker.Application.Interfaces.Services
{
    public interface IWeatherService
    {
        IReadOnlyCollection<CityWeather> GetCitiesWeather(IReadOnlyCollection<string> cityNames);
        IReadOnlyCollection<City> GetAllCities();
    }
}
