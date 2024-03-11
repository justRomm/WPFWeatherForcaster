using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Media;
using System;
using System.Windows.Input;

namespace WeatherForecaster.Styles
{
    public class ForecastButton : Button
    {
        public static readonly DependencyProperty DateDp = DependencyProperty.Register("Date", typeof(string), typeof(ForecastButton));

        public string Date
        {
            get => (string)GetValue(DateDp);
            set => SetValue(DateDp, value);
        }

        public static readonly DependencyProperty DayTempDp = DependencyProperty.Register("DayTemp", typeof(double), typeof(ForecastButton));

        public double DayTemp
        {
            get => (double)GetValue(DayTempDp);
            set => SetValue(DayTempDp, value);
        }

        public static readonly DependencyProperty NightTempDp = DependencyProperty.Register("NightTemp", typeof(double), typeof(ForecastButton));

        public double NightTemp
        {
            get => (double)GetValue(NightTempDp);
            set => SetValue(NightTempDp, value);
        }

        public ForecastButton() : base()
        {

        }
    }
}