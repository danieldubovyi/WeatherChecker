using FluentAssertions;
using Microsoft.Extensions.Options;
using WeatherChecker.Domain.Entities;
using WeatherChecker.Infrastructure.Configuration;

namespace WeatherChecker.Tests
{
    public class CityDataProviderTests
    {
        private CityDataProvider sut;
        private readonly City[] cities;
        public CityDataProviderTests()
        {
            cities = new[]
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
            var options = new CitiesOptions
            {
                Items = cities
            };
            sut = new CityDataProvider(Options.Create(options));
        }

       [Fact]
        public void GetCities_ListOfCities_CitiesReturned()
        {
            //Arrange


            //Act
            var result = sut.GetCities();

            //Assert
            result.Should().BeEquivalentTo(cities);
        }

    }
}
