using WeatherChecker.Domain.Entities;

namespace WeatherChecker.Application.Interfaces.DataProviders
{
    public interface ICityDataProvider
    {
        IReadOnlyCollection<City> GetCities();
    }
}
