using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models;
using WeatherAPI;
namespace WeatherConsoleTester
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var api = new WeatherAPI.WeatherAPI();
                api.StartServer();
            }
            catch(Exception ex)
            {

            }
           
        }

    }
}
