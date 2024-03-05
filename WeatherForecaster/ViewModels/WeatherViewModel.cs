using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherForecaster.Rest.JsonClasses;

namespace WeatherForecaster.ViewModels
{
    public class WeatherViewModel(): DependencyObject
    {
        private static DependencyProperty DescriptionDp = DependencyProperty.Register("Description", typeof(string), typeof(WeatherViewModel));

        public string Description
        {
            get => (GetValue(DescriptionDp) as string);
            set => SetValue(DescriptionDp, value);
        }

        private static DependencyProperty TempDp = DependencyProperty.Register("Temp", typeof(double), typeof(WeatherViewModel));

        public double Temp
        {
            get => ((double)GetValue(TempDp));
            set => SetValue(TempDp, value);
        }

        private static DependencyProperty TempFeelsLikeDp = DependencyProperty.Register("TempFeelsLike", typeof(double), typeof(WeatherViewModel));

        public double TempFeelsLike
        {
            get => ((double)GetValue(TempFeelsLikeDp));
            set => SetValue(TempFeelsLikeDp, value);
        }

        private static DependencyProperty VisibilityDp = DependencyProperty.Register("Visibility", typeof(int), typeof(WeatherViewModel));

        public int Visibility
        {
            get => ((int)GetValue(VisibilityDp));
            set => SetValue(VisibilityDp, value);
        }

        private static DependencyProperty WindSpeedDP = DependencyProperty.Register("WindSpeed", typeof(double), typeof(WeatherViewModel));

        public double WindSpeed
        {
            get => ((double)GetValue(WindSpeedDP));
            set => SetValue(WindSpeedDP, value);
        }

        private static DependencyProperty CloudinessDP = DependencyProperty.Register("Cloudiness", typeof(int), typeof(WeatherViewModel));

        public int Cloudiness
        {
            get => ((int)GetValue(CloudinessDP));
            set => SetValue(CloudinessDP, value);
        }

        private static DependencyProperty UpdateHourDp = DependencyProperty.Register("UpdateHour", typeof(int), typeof(WeatherViewModel));

        public int UpdateHour
        {
            get => ((int)GetValue(UpdateHourDp));
            set => SetValue(UpdateHourDp, value);
        }

        private static DependencyProperty UpdateMinuteDp = DependencyProperty.Register("UpdateMinute", typeof(int), typeof(WeatherViewModel));

        public int UpdateMinute
        {
            get => ((int)GetValue(UpdateMinuteDp));
            set => SetValue(UpdateMinuteDp, value);
        }

        private static DependencyProperty PressureDP = DependencyProperty.Register("Pressure", typeof(int), typeof(WeatherViewModel));

        public int Pressure
        {
            get => ((int)GetValue(PressureDP));
            set => SetValue(PressureDP, value);
        }

        private static DependencyProperty HumidityDp = DependencyProperty.Register("Humidity", typeof(int), typeof(WeatherViewModel));

        public int Humidity
        {
            get => ((int)GetValue(HumidityDp));
            set => SetValue(HumidityDp, value);
        }
    }
}
