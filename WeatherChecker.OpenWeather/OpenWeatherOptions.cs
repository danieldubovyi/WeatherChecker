namespace WeatherChecker.Infrastructure.OpenWeather
{
    public class OpenWeatherOptions
    {
        public const string SectionName = "OpenWeather";
        public string BaseAdress { get; set; }
        public string AppId { get; set; }
    }
}
