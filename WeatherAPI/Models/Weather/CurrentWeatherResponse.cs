using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI
{
    public class CurrentWeatherResponse
    {
        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lon")]
        public float Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }

        [JsonProperty("data")]
        public List<WeatherData> Data { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("dt")]
        public long DateTime { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("dew_point")]
        public float DewPoint { get; set; }

        [JsonProperty("uvi")]
        public float Uvi { get; set; }

        [JsonProperty("clouds")]
        public int Clouds { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public float WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public int WindDegree { get; set; }

        [JsonProperty("weather")]
        public List<WeatherInfo> Weather { get; set; }
    }

    public class WeatherInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}