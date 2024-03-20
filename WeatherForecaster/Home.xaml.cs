using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ABI.Windows.Devices.Sensors;
using WeatherForecaster.DataProtection;
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

        UpdateInfo();

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
            var latAndLong = RestClient.GetInstance().GetLatAndLonFromCity(m_citiesViewModel.SelectedCity);
            currentWeather = RestClient.GetInstance().GetCurrentWeather(latAndLong[0].latitude, latAndLong[0].longitude);
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

        ((MainWindow)Application.Current.MainWindow).UpdateLayout();
    }

    private void AddForecastOnAPanel()
    {
        foreach (var btn in m_forecastViewModel.HourlyForecast.Select(item => new ForecastButton
                 {
                     Margin = new Thickness(5),
                     Width = 100,
                     Date = String.Concat(item.DtTxt.Split(" ")[1][..^3]),
                     DayTemp = item.Main.Temp,
                     // NightTemp = item.Main.Temp - 2,
                     Style = FindResource("ForecastButtonStyle") as Style
                 }))
        {
            ForecastPanel.Children.Add(btn);
        }
    }

    private void InitializeForecast(WeatherForecast data = null)
    {
        var forecast = data;

        if (forecast == null)
        {
            var latAndLong = RestClient.GetInstance().GetLatAndLonFromCity(m_citiesViewModel.SelectedCity);
            forecast = RestClient.GetInstance().GetFiveDayForecast(latAndLong[0].latitude, latAndLong[0].longitude);
        }

        m_forecastViewModel.HourlyForecast = forecast.ForecastList;
        if (ForecastPanel.Children.Count > 0)
        {
            ForecastPanel.Children.Clear();
        }
        AddForecastOnAPanel();
    }

    private void StartAnimation()
    {
        LoadingMessage.Visibility = Visibility.Visible;
    }

    private void StopAnimation()
    {
        LoadingMessage.Visibility = Visibility.Collapsed;
    }

    private async void UpdateInfo()
    {
        Dispatcher.Invoke(StartAnimation);
        var city = m_citiesViewModel.SelectedCity;
        var latAndLong = RestClient.GetInstance().GetLatAndLonFromCity(city);
        CurrentWeather currentWeather = null;
        WeatherForecast forecast = null;

        Task taskResults = null;
        try
        {
            taskResults = Task.WhenAll(
                Task.Run(() =>
                {
                    currentWeather = RestClient.GetInstance().GetCurrentWeather(latAndLong[0].latitude, latAndLong[0].longitude);
                }),
                Task.Run(() =>
                {
                    forecast = RestClient.GetInstance().GetFiveDayForecast(latAndLong[0].latitude, latAndLong[0].longitude);
                })
            );
            await taskResults;
        }
        catch (Exception e)
        {
            AggregateException exception = taskResults.Exception;
            foreach (var error in exception.InnerExceptions)
            {
                Debug.WriteLine(error);
            }
            Debug.WriteLine(e);
        }

        Dispatcher.Invoke(() =>
        {
            InitializeWeather(currentWeather);
            InitializeForecast(forecast);
            StopAnimation();
        });
    }
}