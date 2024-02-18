using System;
using Newtonsoft.Json;

namespace WeatherAPI
{
    public class DailyWeatherResponse
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("tz")]
        public string TimeZone { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("cloud_cover")]
        public CloudCoverInfo CloudCover { get; set; }

        [JsonProperty("humidity")]
        public HumidityInfo Humidity { get; set; }

        [JsonProperty("precipitation")]
        public PrecipitationInfo Precipitation { get; set; }

        [JsonProperty("temperature")]
        public TemperatureInfo Temperature { get; set; }

        [JsonProperty("pressure")]
        public PressureInfo Pressure { get; set; }

        [JsonProperty("wind")]
        public WindInfo Wind { get; set; }

        public class CloudCoverInfo
        {
            [JsonProperty("afternoon")]
            public double Afternoon { get; set; }
        }

        public class HumidityInfo
        {
            [JsonProperty("afternoon")]
            public double Afternoon { get; set; }
        }

        public class PrecipitationInfo
        {
            [JsonProperty("total")]
            public double Total { get; set; }
        }

        public class TemperatureInfo
        {
            [JsonProperty("min")]
            public double Min { get; set; }

            [JsonProperty("max")]
            public double Max { get; set; }

            [JsonProperty("afternoon")]
            public double Afternoon { get; set; }

            [JsonProperty("night")]
            public double Night { get; set; }

            [JsonProperty("evening")]
            public double Evening { get; set; }

            [JsonProperty("morning")]
            public double Morning { get; set; }
        }

        public class PressureInfo
        {
            [JsonProperty("afternoon")]
            public double Afternoon { get; set; }
        }

        public class WindInfo
        {
            [JsonProperty("max")]
            public MaxWindInfo Max { get; set; }

            public class MaxWindInfo
            {
                [JsonProperty("speed")]
                public double Speed { get; set; }

                [JsonProperty("direction")]
                public double Direction { get; set; }
            }
        }
    }

}
