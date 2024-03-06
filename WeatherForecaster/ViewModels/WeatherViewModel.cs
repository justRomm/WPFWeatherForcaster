using System;
using System.Windows;

namespace WeatherForecaster.ViewModels;

public class WeatherViewModel : DependencyObject
{
    private static readonly DependencyProperty DescriptionDp =
        DependencyProperty.Register("Description", typeof(string), typeof(WeatherViewModel));

    private static readonly DependencyProperty TempDp =
        DependencyProperty.Register("Temperature", typeof(double), typeof(WeatherViewModel));

    private static readonly DependencyProperty TempFeelsLikeDp =
        DependencyProperty.Register("TempFeelsLike", typeof(double), typeof(WeatherViewModel));

    private static readonly DependencyProperty VisibilityDp =
        DependencyProperty.Register("Visibility", typeof(int), typeof(WeatherViewModel));

    private static readonly DependencyProperty WindSpeedDP =
        DependencyProperty.Register("WindSpeed", typeof(double), typeof(WeatherViewModel));

    private static readonly DependencyProperty CloudinessDP =
        DependencyProperty.Register("Cloudiness", typeof(int), typeof(WeatherViewModel));

    private static readonly DependencyProperty UpdateHourDp =
        DependencyProperty.Register("UpdateHour", typeof(int), typeof(WeatherViewModel));

    private static readonly DependencyProperty UpdateMinuteDp =
        DependencyProperty.Register("UpdateMinute", typeof(int), typeof(WeatherViewModel));

    private static readonly DependencyProperty PressureDP =
        DependencyProperty.Register("Pressure", typeof(int), typeof(WeatherViewModel));

    private static readonly DependencyProperty HumidityDp =
        DependencyProperty.Register("Humidity", typeof(int), typeof(WeatherViewModel));

    private static readonly DependencyProperty DewPointDp =
        DependencyProperty.Register("DewPoint", typeof(int), typeof(WeatherViewModel));

    public string Description
    {
        get => GetValue(DescriptionDp) as string;
        set => SetValue(DescriptionDp, value);
    }

    public double Temperature
    {
        get => (double)GetValue(TempDp);
        set => SetValue(TempDp, value);
    }

    public double TempFeelsLike
    {
        get => (double)GetValue(TempFeelsLikeDp);
        set => SetValue(TempFeelsLikeDp, value);
    }

    public int Visibility
    {
        get => (int)GetValue(VisibilityDp);
        set => SetValue(VisibilityDp, value);
    }

    public double WindSpeed
    {
        get => (double)GetValue(WindSpeedDP);
        set => SetValue(WindSpeedDP, value);
    }

    public int Cloudiness
    {
        get => (int)GetValue(CloudinessDP);
        set => SetValue(CloudinessDP, value);
    }

    public int UpdateHour
    {
        get => (int)GetValue(UpdateHourDp);
        set => SetValue(UpdateHourDp, value);
    }

    public int UpdateMinute
    {
        get => (int)GetValue(UpdateMinuteDp);
        set => SetValue(UpdateMinuteDp, value);
    }

    public int Pressure
    {
        get => (int)GetValue(PressureDP);
        set => SetValue(PressureDP, value);
    }

    public int Humidity
    {
        get => (int)GetValue(HumidityDp);
        set => SetValue(HumidityDp, value);
    }

    public int DewPoint
    {
        get => (int)GetValue(PressureDP);
        set => SetValue(DewPointDp, value);
    }

    
}