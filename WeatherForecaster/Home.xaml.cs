using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherForecaster.ViewModels;
using WeatherForecaster.Rest;

namespace WeatherForecaster
{
    public partial class Home : Page
    {
        private readonly CitiesViewModel _citiesViewModel;
        private readonly WeatherViewModel _weatherViewModel = new();
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

        public Home(CitiesViewModel ctv)
        {
            _citiesViewModel = ctv;

            InitializeComponent();

            InitializeWeather();
            this.DataContext = new
            {
                cities = _citiesViewModel,
                weather = _weatherViewModel
            };

        }
        
        private void InitializeWeather()
        {
            var latAndLong = RestClient.GetLatAndLonFromCity(_citiesViewModel.SelectedCity);
            var currentWeather = RestClient.GetCurrentWeather(latAndLong[0].latitude, latAndLong[0].longitude);

            _weatherViewModel.Temperature = currentWeather.main.temp;
            _weatherViewModel.Description = currentWeather.weather[0].main;
            _weatherViewModel.Cloudiness = currentWeather.clouds.all;
            _weatherViewModel.Humidity = currentWeather.main.humidity;
            _weatherViewModel.Visibility = currentWeather.visibility;
            _weatherViewModel.WindSpeed = currentWeather.wind.speed;
            _weatherViewModel.TempFeelsLike = currentWeather.main.feels_like;
            _weatherViewModel.Pressure = currentWeather.main.pressure;
            _weatherViewModel.DewPoint = CalculateDewPoint(_weatherViewModel.Humidity, _weatherViewModel.Temperature);

            var currentTime = DateTime.Now;

            _weatherViewModel.UpdateHour = currentTime.Hour;
            _weatherViewModel.UpdateMinute = currentTime.Minute;
        }
    }
}
