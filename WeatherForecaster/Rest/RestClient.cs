using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WeatherForecaster.Rest.JsonClasses;

namespace WeatherForecaster.Rest
{
    public static class RestClient
    {
        private const string _APIKey = "fe14f9bc8fcee04ee1fdb69408290496";
        private const int _maxTimeout = 20000;
        private const string _weatherUrl = "https://api.openweathermap.org/data/2.5";
        private const string _geoUrl = "http://api.openweathermap.org/geo/1.0";
        
        private static void ThrowOnErrorResponse(RestResponse response)
        {
            if (!response.IsSuccessful || response.StatusCode != HttpStatusCode.OK)
            {
                throw new RestClientErrorException(response);
            }
        }
        
        public static CurrentWeather GetCurrentWeather(double lat, double lon)
        {
            using var client = new RestSharp.RestClient(new RestClientOptions
            {
                MaxTimeout = _maxTimeout,
                BaseUrl = new Uri($"{_weatherUrl}/weather?lat={lat}&lon={lon}&units=metric&appid={_APIKey}")
            });
            var request = new RestRequest("", Method.Get);
            RestResponse restResponse = client.Execute(request);

            ThrowOnErrorResponse(restResponse);

            return JsonConvert.DeserializeObject<CurrentWeather>(restResponse.Content);
        }

        public static List<Location> GetLatAndLonFromCity(string city)
        {
            using var client = new RestSharp.RestClient(new RestClientOptions
            {
                MaxTimeout = _maxTimeout,
                BaseUrl = new Uri($"{_geoUrl}/direct?q={city}&limit=1&appid={_APIKey}")
            });
            var request = new RestRequest("", Method.Get);
            RestResponse restResponse = client.Execute(request);

            ThrowOnErrorResponse(restResponse);

            return JsonConvert.DeserializeObject<List<Location>>(restResponse.Content);
        }


        public static AirPollution GetAirPollution(double lat, double lon)
        {
            using var client = new RestSharp.RestClient(new RestClientOptions
            {
                MaxTimeout = _maxTimeout,
                BaseUrl = new Uri($"{_weatherUrl}/air_pollution?lat={lat}&lon={lon}&units=metric&appid={_APIKey}")
            });
            var request = new RestRequest("", Method.Get);
            RestResponse restResponse = client.Execute(request);


            ThrowOnErrorResponse(restResponse);

            return JsonConvert.DeserializeObject<AirPollution>(restResponse.Content);
        }

        public static WeatherForecast GetFiveDayForecast(double lat, double lon)
        {
            using var client = new RestSharp.RestClient(new RestClientOptions
            {
                MaxTimeout = _maxTimeout,
                BaseUrl = new Uri($"{_weatherUrl}/forecast?lat={lat}&lon={lon}&units=metric&appid={_APIKey}")
            });
            var request = new RestRequest("", Method.Get);
            RestResponse restResponse = client.Execute(request);


            ThrowOnErrorResponse(restResponse);

            return JsonConvert.DeserializeObject<WeatherForecast>(restResponse.Content);
        }
    }
}
