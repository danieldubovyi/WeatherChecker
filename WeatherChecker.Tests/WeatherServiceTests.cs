using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.Application.Interfaces.DataProviders;
using WeatherChecker.Application.Services;
using WeatherChecker.Domain.Entities;

namespace WeatherChecker.Tests
{
    public class WeatherServiceTests
    {
        private readonly WeatherService sut;
        private readonly Mock<IWeatherDataProvider> weatherDataProviderMock;
        private readonly Mock<ICityDataProvider> cityDataProviderMock;

        public WeatherServiceTests()
        {
            
            cityDataProviderMock = new Mock<ICityDataProvider>(MockBehavior.Strict);
            weatherDataProviderMock = new Mock<IWeatherDataProvider>(MockBehavior.Strict);
            sut = new WeatherService(cityDataProviderMock.Object, weatherDataProviderMock.Object);
        }

        [Fact]
        public void GetAllCities_ListOfCities_CitiesReturned()
        {
            //Arrange
            var cities = new[]
            {
                new City
                {
                    Name = "Warsaw",
                    Lat = 52.2319581m,
                    Lon = 21.0067249m
                },
                new City
                {
                    Name = "London",
                    Lat = 51.5073219m,
                    Lon = -0.1276474m
                }
            };
            cityDataProviderMock.Setup(m => m.GetCities())
                .Returns(cities);

            //Act
            var result = sut.GetAllCities();

            //Assert
            result.Should().BeEquivalentTo(cities);
        }

        [Theory]
        [InlineData(0, 0, 0, "Słonecznie")]
        [InlineData(20, 0, 0, "Słońce za chmurami")]
        [InlineData(70, 0, 0, "Chmury")]
        [InlineData(70, 10, 0, "Deszcz")]
        [InlineData(70, 0, 10, "Śnieg")]
        public void GetCitiesWeather_ValidParams_ExpectedDescriptionReturned(decimal clouds, decimal rain, decimal snow, string expectedDescription)
        {
            //Arrange
            var city = new City { Name = "Warsaw", Lat = 52.2319581m, Lon = 21.0067249m };
            cityDataProviderMock.Setup(m => m.GetCities())
                .Returns(new[] {city});
            weatherDataProviderMock.Setup(m => m.GetWeatherData(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(new Weather { Temp = 273, Clouds = clouds, Rain = rain, Snow = snow });

            //Act
            var result = sut.GetCitiesWeather(new[] { city.Name });

            //Assert
            result.Should().HaveCount(1);
            result.First().Description.Should().Be(expectedDescription);
        }

        [Theory]
        [InlineData(273, 0)]
        [InlineData(300, 27)]
        [InlineData(290.3, 17.3)]
        public void GetCitiesWeather_ValidParams_TemperatureConvertedToCelsius(decimal tempKelvin, decimal tempCelsius)
        {
            //Arrange
            var city = new City { Name = "Warsaw", Lat = 52.2319581m, Lon = 21.0067249m };
            cityDataProviderMock.Setup(m => m.GetCities())
                .Returns(new[] { city });
            weatherDataProviderMock.Setup(m => m.GetWeatherData(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(new Weather { Temp = tempKelvin, Clouds = 0, Rain = 0, Snow = 0 });

            //Act
            var result = sut.GetCitiesWeather(new[] { city.Name });

            //Assert
            result.Should().HaveCount(1);
            result.First().TempInCelsius.Should().Be(tempCelsius);
        }
    }
}
