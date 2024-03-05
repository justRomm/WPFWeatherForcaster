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
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private CitiesViewModel _citiesViewModel;
        private WeatherViewModel _weatherViewModel = new();
        public Home(CitiesViewModel ctv)
        {
            _citiesViewModel = ctv;

            InitializeComponent();

            InitializeTemp();
            this.DataContext = new
            {
                cities = _citiesViewModel,
                weather = _weatherViewModel
            };

        }

        private void InitializeTemp()
        {

            var latAndLong = RestClient.GetLatAndLonFromCity(_citiesViewModel.SelectedCity);
            var currentWeather = RestClient.GetCurrentWeather(latAndLong[0].latitude, latAndLong[0].longitude);

            _weatherViewModel.Temp = currentWeather.main.temp;
            _weatherViewModel.Description = currentWeather.weather[0].main;
            _weatherViewModel.Cloudiness = currentWeather.clouds.all;
            _weatherViewModel.Humidity = currentWeather.main.humidity;
            _weatherViewModel.Visibility = currentWeather.visibility;
            _weatherViewModel.WindSpeed = currentWeather.wind.speed;
            _weatherViewModel.TempFeelsLike = currentWeather.main.feels_like;
            _weatherViewModel.Pressure = currentWeather.main.pressure;

            var currentTime = DateTime.Now;

            _weatherViewModel.UpdateHour = currentTime.Hour;
            _weatherViewModel.UpdateMinute = currentTime.Minute;



        }

        
    }


}
