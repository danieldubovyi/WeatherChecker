using FluentAssertions;
using Microsoft.Extensions.Options;
using WeatherChecker.Infrastructure.OpenWeather;

namespace WeatherChecker.Tests
{
    public class WeatherDataProviderTests
    {
        private WeatherDataProvider sut;

        public WeatherDataProviderTests()
        {
            HttpClient client = new HttpClient();
            OpenWeatherOptions openWeatherOptions = new()
            {
                BaseAdress = "http://api.openweathermap.org",
                AppId = "93836445ebca1a81c1081e8b9fa753f0"
            };
            sut = new WeatherDataProvider(client, Options.Create(openWeatherOptions));
        }

        [Fact]
        public void GetWeatherData_ValidRequest_WeatherObjectReturned()
        {
            //Arrange
            decimal lat = 33.44m;
            decimal lon = -94.04m;

            //Act
            var result = sut.GetWeatherData(lat, lon);

            //Assert
            result.Should().NotBeNull();
            result.Temp.Should().BeGreaterThan(0);
            result.Clouds.Should().BeGreaterThan(-1);
            result.Rain.Should().BeGreaterThan(-1);
            result.Snow.Should().BeGreaterThan(-1);
        }

        [Fact]
        public void GetWeatherData_UnknownPlace_WeatherObjectReturned()
        {
            //Arrange
            decimal lat = 1000m;
            decimal lon = -94.04m;

            //Act
            Action action = () => sut.GetWeatherData(lat, lon);

            //Assert
            action.Should().Throw<Exception>();
        }
    }
}