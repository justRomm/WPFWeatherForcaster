using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WeatherForecaster.Rest;
using WeatherForecaster.Rest.JsonClasses;

namespace WeatherForecaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var tmp = RestClient.GetLatAndLonFromCity("London");

            Console.WriteLine($"{tmp[0].latitude} {tmp[0].longitude}");

        }
    }
}
