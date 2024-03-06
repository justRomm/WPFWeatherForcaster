using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ABI.Windows.Devices.Sensors;
using WeatherForecaster.Rest;
using WeatherForecaster.Rest.JsonClasses;
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

        m_citiesViewModel.SelectedCityChanged += UpdateInfo;
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

    private void InitializeWeather(CurrentWeather givenWeather = null)
    {
        var currentWeather = givenWeather;

        if (currentWeather == null)
        {
            currentWeather = RequestWeather(m_citiesViewModel.SelectedCity);
        }

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

        ((MainWindow)System.Windows.Application.Current.MainWindow).UpdateLayout();
    }

    private CurrentWeather RequestWeather(string city)
    {
        var latAndLong = RestClient.GetLatAndLonFromCity(city);
        return RestClient.GetCurrentWeather(latAndLong[0].latitude, latAndLong[0].longitude);
    }

    private void StartAnimation()
    {
        LoadingMessage.Visibility = Visibility.Visible;
        System.Diagnostics.Debug.WriteLine("Changed");
    }

    private void StopAnimation()
    {
        LoadingMessage.Visibility = Visibility.Collapsed;
        System.Diagnostics.Debug.WriteLine("Changed");
    }

    private void UpdateInfo()
    {
        Dispatcher.Invoke(StartAnimation);
        var city = m_citiesViewModel.SelectedCity;
        Task.Run(() =>
        {
            var data = RequestWeather(city);

            Dispatcher.Invoke(() =>
            {
                InitializeWeather(data);
                StopAnimation();
            });
        });
    }
}