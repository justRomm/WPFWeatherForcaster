using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ABI.Windows.Devices.Sensors;
using WeatherForecaster.Rest;
using WeatherForecaster.Rest.JsonClasses;
using WeatherForecaster.Styles;
using WeatherForecaster.ViewModels;

namespace WeatherForecaster;

public partial class Home : Page
{
    private readonly CitiesViewModel m_citiesViewModel;
    private readonly CurrentWeatherViewModel m_currentWeatherViewModel = new();
    private readonly ForecastViewModel m_forecastViewModel = new();

    public Home(CitiesViewModel ctv)
    {
        m_citiesViewModel = ctv;

        InitializeComponent();

        InitializeWeather();
        InitializeForecast();

        DataContext = new
        {
            cities = m_citiesViewModel,
            weather = m_currentWeatherViewModel
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

        m_currentWeatherViewModel.Temperature = currentWeather.main.temp;
        m_currentWeatherViewModel.Description = currentWeather.weather[0].main;
        m_currentWeatherViewModel.Cloudiness = currentWeather.clouds.all;
        m_currentWeatherViewModel.Humidity = currentWeather.main.humidity;
        m_currentWeatherViewModel.Visibility = currentWeather.visibility;
        m_currentWeatherViewModel.WindSpeed = currentWeather.wind.speed;
        m_currentWeatherViewModel.TempFeelsLike = currentWeather.main.feels_like;
        m_currentWeatherViewModel.Pressure = currentWeather.main.pressure;
        m_currentWeatherViewModel.DewPoint = CalculateDewPoint(m_currentWeatherViewModel.Humidity, m_currentWeatherViewModel.Temperature);

        var currentTime = DateTime.Now;

        m_currentWeatherViewModel.UpdateHour = currentTime.Hour;
        m_currentWeatherViewModel.UpdateMinute = currentTime.Minute;

        ((MainWindow)System.Windows.Application.Current.MainWindow).UpdateLayout();
    }

    private void AddForecastOnAPanel()
    {
        foreach (var btn in m_forecastViewModel.HourlyForecast.Select(item => new ForecastButton
                 {
                     Margin = new Thickness(5),
                     Width = 100,
                     Date = String.Concat(item.DtTxt.Split(" ")[1].Split(":")[0], ':', item.DtTxt.Split(" ")[1].Split(":")[1]),
                     DayTemp = item.Main.Temp,
                     NightTemp = item.Main.Temp - 2,
                     Style = FindResource("ForecastButtonStyle") as Style
                 }))
        {
            ForecastPanel.Children.Add(btn);
        }
    }

    private void InitializeForecast()
    {
        var latAndLong = RestClient.GetLatAndLonFromCity("London");
        var forecast = RestClient.GetFiveDayForecast(latAndLong[0].latitude, latAndLong[0].longitude);

        m_forecastViewModel.HourlyForecast = forecast.ForecastList;

        AddForecastOnAPanel();
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