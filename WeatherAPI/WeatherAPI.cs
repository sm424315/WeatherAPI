using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestSharp;
using WeatherAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
using System.IO;

namespace WeatherAPI
{
    public class WeatherAPI
    {
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        string apiKey = "";
        public static async Task Main(string[] args)
        {
            try
            {         
                var api = new WeatherAPI();
                api.StartServer();
            }
            catch (Exception ex)
            {

            }

        }

        public void StartServer()
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }


        public async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                string responseData = "";
                string lat = "";
                string lon = "";
                int time = 0;
                string date = "";

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                
                try
                {
                    if (req.HttpMethod == "GET" && req.Url.AbsolutePath == "/api/CurrentWeather/")  // Current Weather Endpoint
                    {
                        //Using the QueryString, obtain our data to be used to call OpenWeatherAPI
                        lat = req.QueryString.Get("lat");
                        lon = req.QueryString.Get("lon");
                        time = int.Parse(req.QueryString.Get("time"));

                        // Get the current weather using the lat/lon and time provided
                        var weather = await GetCurrentWeather(lat, lon, time);

                        // Alter raw data into standardized weather data, as depicted on websites
                        weather.Data[0].Temperature = (weather.Data[0].Temperature - 273.15) * 9 / 5 + 31;  // k to f
                        weather.Data[0].FeelsLike = (weather.Data[0].FeelsLike - 273.15) * 9 / 5 + 31;  // k to f
                        weather.Data[0].Pressure = Math.Round(weather.Data[0].Pressure * 0.02953, 2);  // mb to inHg

                        ReturnRequest(resp, weather);
                    }
                    else if(req.HttpMethod == "GET" && req.Url.AbsolutePath == "/api/DailyWeather/")  // DailyWeather Endpoint
                    {
                        lat = req.QueryString.Get("lat");
                        lon = req.QueryString.Get("lon");
                        date = req.QueryString.Get("date");

                        // Get the current weather using the lat/lon and time provided
                        var weather = await GetDailyWeather(lat, lon, date);

                        // Alter raw data into standardized weather data, as depicted on websites
                        weather.Temperature.Min = (weather.Temperature.Min - 273.15) * 9 / 5 + 31;  // k to f
                        weather.Temperature.Max = (weather.Temperature.Max - 273.15) * 9 / 5 + 31;  // k to f

                        ReturnRequest(resp, weather);
                    }
                }
                catch (Exception ex) {

                    byte[] buffer = Encoding.UTF8.GetBytes(ex.ToString());
                    resp.ContentType = "text/xml";
                    resp.ContentLength64 = buffer.Length;
                    resp.StatusCode = (int)HttpStatusCode.OK;
                    resp.OutputStream.Write(buffer, 0, buffer.Length);
                }          
            }
        }

        private async void ReturnRequest(HttpListenerResponse response, object weather)
        {
            // Create Response
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(weather);
            byte[] buffer = Encoding.UTF8.GetBytes(jsonData);
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.OutputStream.Write(buffer, 0, buffer.Length);

            // Write out to the response stream (asynchronously), then close it
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close();
        }

        public async Task<DailyWeatherResponse> GetDailyWeather(string latitude, string longitude, string date)
        {
           
            // Get my API Key
            using (StreamReader reader = new StreamReader("C://Users/bryce/OneDrive/Documents/apiKey.txt"))
            {
                apiKey = reader.ReadLine();
            }
            
            // Construct the URL with query parameters
            string apiUrl = $"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={latitude}&lon={longitude}&date={date}&appid={apiKey}";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseBody = await response.Content.ReadAsStringAsync();

                        DailyWeatherResponse result = JsonConvert.DeserializeObject<DailyWeatherResponse>(responseBody);

                        return result;
                    }
                    else
                    {
                        var x = await response.Content.ReadAsStringAsync();
                        throw new HttpRequestException(response.ReasonPhrase, new Exception(x));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<CurrentWeatherResponse> GetCurrentWeather(string latitude, string longitude, int time)
        {
            // Get my API Key
            using (StreamReader reader = new StreamReader("C://Users/bryce/OneDrive/Documents/apiKey.txt"))
            {
                apiKey = reader.ReadLine();
            }

            // Construct the URL with query parameters
            string apiUrl = $"https://api.openweathermap.org/data/3.0/onecall/timemachine?lat={latitude}&lon={longitude}&dt={time}&appid={apiKey}";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseBody = await response.Content.ReadAsStringAsync();

                        CurrentWeatherResponse result = JsonConvert.DeserializeObject<CurrentWeatherResponse>(responseBody);

                        return result;
                    }
                    else
                    {
                        var x = await response.Content.ReadAsStringAsync();
                        throw new HttpRequestException(response.ReasonPhrase, new Exception(x));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
           
        }
    }
}
