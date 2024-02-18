using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI
{
    public interface IWeatherAPI
    {
        void StartServer();
        Task<DailyWeatherResponse> GetDailyWeather(string lat, string lon, DateTime date);
        Task<CurrentWeatherResponse> GetCurrentWeather(string lat, string lon, int time);
    }
}
