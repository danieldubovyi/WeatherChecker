using WeatherChecker.Domain.Entities;

namespace WeatherChecker.Infrastructure.Configuration
{
    public class CitiesOptions
    {
        public const string SectionName = "Cities";
        public City[] Items { get; set; }
    }
}
