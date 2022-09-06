using Microsoft.Extensions.Options;
using WeatherChecker.Application.Interfaces.DataProviders;
using WeatherChecker.Domain.Entities;

namespace WeatherChecker.Infrastructure.Configuration
{
    public class CityDataProvider : ICityDataProvider
    {
        readonly City[] cities;
        public CityDataProvider(IOptions<CitiesOptions> citiesOptions)
        {
            cities = citiesOptions.Value.Items;
        }

        public IReadOnlyCollection<City> GetCities()
        {
            return cities;    
        }
    }
}
