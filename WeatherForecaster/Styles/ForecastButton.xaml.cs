using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Media;
using System;
using System.Windows.Input;
using System.Reflection.PortableExecutable;

namespace WeatherForecaster.Styles
{
    public class ForecastButton : Button
    {
        public static readonly DependencyProperty DateDp = DependencyProperty.Register("Date", typeof(string), typeof(ForecastButton));

        public string Date
        {
            set
            {
                SetValue(DateDp, value);
                SetValue(DateHoursDp, String.Concat(value.Split(" ")[1][..^3]));
                var date = value.Split(" ")[0];
                DateTime dateValue = new DateTime(System.Int32.Parse(date[..4]), System.Int32.Parse(date[5..7]), System.Int32.Parse(date[8..]));

                SetValue(WeekdayDp, dateValue.ToString("dddd"));
            }
        }

        public static readonly DependencyProperty DateHoursDp = DependencyProperty.Register("DateHours", typeof(string), typeof(ForecastButton));

        public string DateHours
        {
            get => (string)GetValue(DateHoursDp);
        }

        public static readonly DependencyProperty WeekdayDp = DependencyProperty.Register("Weekday", typeof(string), typeof(ForecastButton));

        public string Weekday
        {
            get => (string)GetValue(WeekdayDp);
        }

        public static readonly DependencyProperty DayTempDp = DependencyProperty.Register("DayTemp", typeof(double), typeof(ForecastButton));

        public double DayTemp
        {
            get => (double)GetValue(DayTempDp);
            set => SetValue(DayTempDp, value);
        }

        //public static readonly DependencyProperty NightTempDp = DependencyProperty.Register("NightTemp", typeof(double), typeof(ForecastButton));

        //public double NightTemp
        //{
        //    get => (double)GetValue(NightTempDp);
        //    set => SetValue(NightTempDp, value);
        //}

        public ForecastButton() : base()
        {

        }
    }
}