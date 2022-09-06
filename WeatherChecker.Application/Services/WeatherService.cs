using WeatherChecker.Application.Interfaces.DataProviders;
using WeatherChecker.Application.Interfaces.Services;
using WeatherChecker.Domain.Entities;

namespace WeatherChecker.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ICityDataProvider _cityDataProvider;
        private readonly IWeatherDataProvider _weatherDataProvider;
        public WeatherService(ICityDataProvider cityDataProvider, IWeatherDataProvider weatherDataProvider)
        {
            _cityDataProvider = cityDataProvider;
            _weatherDataProvider = weatherDataProvider;
        }

        public IReadOnlyCollection<CityWeather> GetCitiesWeather(IReadOnlyCollection<string> cityNames)
        {
            var cityWeathers = _cityDataProvider.GetCities()
                .Where(i => cityNames.Contains(i.Name))
                .Select(GetCityWeather)
                .ToArray();
            return cityWeathers;
        }

        public IReadOnlyCollection<City> GetAllCities()
        {
            return _cityDataProvider.GetCities();
        }

        private CityWeather GetCityWeather(City city)
        {
            var weatherData = _weatherDataProvider.GetWeatherData(city.Lat, city.Lon);

            var clouds = weatherData.Clouds;
            var rain = weatherData.Rain;
            var snow = weatherData.Snow;

            GetWeatherDescription(clouds, rain, snow);

            return new CityWeather
            {
                Name = city.Name,
                TempInCelsius = weatherData.Temp - 273,
                Description = GetWeatherDescription(clouds, rain, snow)
            };
        }

        private string GetWeatherDescription(decimal clouds, decimal rain, decimal snow)
        {
            if (snow == 0 && rain == 0)
            {
                if (clouds <= 10)
                    return "Słonecznie";

                if (clouds <= 60)
                    return "Słońce za chmurami";

                return "Chmury";
            }

            if (rain == 0)
                return "Śnieg";
            return "Deszcz";
        }
    }
}
