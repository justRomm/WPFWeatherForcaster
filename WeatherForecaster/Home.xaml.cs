using System;
using System.Windows.Controls;
using WeatherForecaster.Rest;
using WeatherForecaster.ViewModels;

namespace WeatherForecaster;

public partial class Home : Page
{
    private readonly CitiesViewModel m_citiesViewModel;
    private readonly WeatherViewModel m_weatherViewModel = new();

    public Home(CitiesViewModel ctv)
    {
        m_citiesViewModel = ctv;

        InitializeComponent();

        InitializeWeather();
        DataContext = new
        {
            cities = m_citiesViewModel,
            weather = m_weatherViewModel
        };
    }

    private static int CalculateDewPoint(double humidity, double temperature)
    {
        if (double.IsNaN(humidity) || double.IsNaN(temperature))
        {
            return 0;
        }

        var inner = Math.Log(humidity / 100) + 17.62 * temperature / (243.12 + temperature);
        var res = 243.12 * inner / (17.62 - inner);

        return (int)Math.Round(res);
    }

    private void InitializeWeather()
    {
        var latAndLong = RestClient.GetLatAndLonFromCity(m_citiesViewModel.SelectedCity);
        var currentWeather = RestClient.GetCurrentWeather(latAndLong[0].latitude, latAndLong[0].longitude);

        m_weatherViewModel.Temperature = currentWeather.main.temp;
        m_weatherViewModel.Description = currentWeather.weather[0].main;
        m_weatherViewModel.Cloudiness = currentWeather.clouds.all;
        m_weatherViewModel.Humidity = currentWeather.main.humidity;
        m_weatherViewModel.Visibility = currentWeather.visibility;
        m_weatherViewModel.WindSpeed = currentWeather.wind.speed;
        m_weatherViewModel.TempFeelsLike = currentWeather.main.feels_like;
        m_weatherViewModel.Pressure = currentWeather.main.pressure;
        m_weatherViewModel.DewPoint = CalculateDewPoint(m_weatherViewModel.Humidity, m_weatherViewModel.Temperature);

        var currentTime = DateTime.Now;

        m_weatherViewModel.UpdateHour = currentTime.Hour;
        m_weatherViewModel.UpdateMinute = currentTime.Minute;
    }
}