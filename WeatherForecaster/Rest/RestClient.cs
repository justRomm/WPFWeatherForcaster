using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WeatherForecaster.DataProtection;
using WeatherForecaster.Rest.JsonClasses;

namespace WeatherForecaster.Rest;

public class RestClient
{
    private static string _APIKey;
    private const int _maxTimeout = 20000;
    private const string _weatherUrl = "https://api.openweathermap.org/data/2.5";
    private const string _geoUrl = "http://api.openweathermap.org/geo/1.0";
    private static RestClient _instance;

    private static void ThrowOnErrorResponse(RestResponse response)
    {
        if (!response.IsSuccessful || response.StatusCode != HttpStatusCode.OK)
            throw new RestClientErrorException(response);
    }

    private RestClient()
    {
        var protector = new ApiProtector();
        _APIKey = protector.GetApi();
    }

    public static RestClient GetInstance()
    {
        if (_instance == null)
        {
            _instance = new RestClient();
        }
        return _instance;
    }

    public static CurrentWeather GetCurrentWeather(double lat, double lon)
    {
        using var client = new RestSharp.RestClient(new RestClientOptions
        {
            MaxTimeout = _maxTimeout,
            BaseUrl = new Uri($"{_weatherUrl}/weather?lat={lat}&lon={lon}&units=metric&appid={_APIKey}")
        });
        var request = new RestRequest("");
        var restResponse = client.Execute(request);

        ThrowOnErrorResponse(restResponse);

        return JsonConvert.DeserializeObject<CurrentWeather>(restResponse.Content);
    }

    public async Task<CurrentWeather> GetCurrentWeatherAsync(double lat, double lon)
    {
        using var client = new RestSharp.RestClient(new RestClientOptions
        {
            MaxTimeout = _maxTimeout,
            BaseUrl = new Uri($"{_weatherUrl}/weather?lat={lat}&lon={lon}&units=metric&appid={_APIKey}")
        });
        var request = new RestRequest("");
        var restResponse = await client.ExecuteAsync(request);

        ThrowOnErrorResponse(restResponse);

        return JsonConvert.DeserializeObject<CurrentWeather>(restResponse.Content);
    }

    public List<Location> GetLatAndLonFromCity(string city)
    {
        using var client = new RestSharp.RestClient(new RestClientOptions
        {
            MaxTimeout = _maxTimeout,
            BaseUrl = new Uri($"{_geoUrl}/direct?q={city}&limit=1&appid={_APIKey}")
        });
        var request = new RestRequest("");
        var restResponse = client.Execute(request);

        ThrowOnErrorResponse(restResponse);

        return JsonConvert.DeserializeObject<List<Location>>(restResponse.Content);
    }
    
    public WeatherForecast GetFiveDayForecast(double lat, double lon)
    {
        using var client = new RestSharp.RestClient(new RestClientOptions
        {
            MaxTimeout = _maxTimeout,
            BaseUrl = new Uri($"{_weatherUrl}/forecast?lat={lat}&lon={lon}&units=metric&appid={_APIKey}")
        });

        var request = new RestRequest("");
        var restResponse = client.Execute(request);


        ThrowOnErrorResponse(restResponse);

        return JsonConvert.DeserializeObject<WeatherForecast>(restResponse.Content);
    }
}