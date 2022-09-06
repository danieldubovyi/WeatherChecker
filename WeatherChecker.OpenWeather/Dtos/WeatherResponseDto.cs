using System.Text.Json.Serialization;

namespace WeatherChecker.Infrastructure.OpenWeather.Dtos
{
    internal class WeatherResponseDto
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }

        public CurrentDto Current { get; set; }
    }


    class CurrentDto
    {
        public decimal Temp { get; set; }
        [JsonPropertyName("clouds")]
        public decimal Clouds { get; set; }
        [JsonPropertyName("rain")]
        public PrecipitationDto Rain { get; set; }
        [JsonPropertyName("snow")]
        public PrecipitationDto Snow { get; set; }
    }

    class PrecipitationDto
    {
        [JsonPropertyName("1h")]
        public decimal Value1h { get; set; }
        [JsonPropertyName("3h")]
        public decimal Value3h { get; set; }
    }


}
