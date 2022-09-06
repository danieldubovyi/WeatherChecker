using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherChecker.Application.Interfaces.DataProviders;
using WeatherChecker.Domain.Entities;
using WeatherChecker.Infrastructure.OpenWeather.Dtos;

namespace WeatherChecker.Infrastructure.OpenWeather
{
    public class WeatherDataProvider : IWeatherDataProvider
    {
        readonly HttpClient client;
        readonly OpenWeatherOptions openWeatherOptions;

        public WeatherDataProvider(HttpClient client, IOptions<OpenWeatherOptions> openWeatherOptions)
        {
            this.client = client;
            this.openWeatherOptions = openWeatherOptions.Value;
            client.BaseAddress = new Uri(this.openWeatherOptions.BaseAdress);
        }

        public Weather GetWeatherData(decimal lat, decimal lon)
        {
            string url = $"data/2.5/onecall?lat={lat}&lon={lon}&appid={openWeatherOptions.AppId}&cnt=1;";
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string content = response.Content.ReadAsStringAsync().Result;
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dto = JsonSerializer.Deserialize<WeatherResponseDto>(content, serializeOptions);
            return new Weather
            {
                Temp = dto.Current.Temp,
                Clouds = dto.Current.Clouds,
                Rain = dto.Current.Rain?.Value3h ?? 0,
                Snow = dto.Current.Snow?.Value3h ?? 0
            };
        }
    }
}
