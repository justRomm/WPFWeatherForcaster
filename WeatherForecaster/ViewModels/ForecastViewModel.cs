using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using WeatherForecaster.Rest.JsonClasses;

namespace WeatherForecaster.ViewModels;

public struct DailyForecastItem
{
    public string day;
    public double dayTemperature;
    public double nightTemperature;
    public Clouds clouds;
}

public struct HourlyForecastItem
{
    public string time;
    public double temperature;
    public Clouds clouds;
    public Wind wind;
}

public class ForecastViewModel : DependencyObject
{
    private static readonly DependencyProperty HourlyForecastDp =
        DependencyProperty.Register("HourlyForecast", typeof(List<ForecastWeather>), typeof(ForecastViewModel));

    private static readonly DependencyProperty DailyForecastDp =
        DependencyProperty.Register("DailyForecast", typeof(List<ForecastWeather>), typeof(ForecastViewModel));

    public List<ForecastWeather> HourlyForecast
    {
        get => GetValue(HourlyForecastDp) as List<ForecastWeather>;
        set => SetValue(HourlyForecastDp, value);
    }

    public List<DailyForecastItem> DailyForecast
    {
        get => GetValue(DailyForecastDp) as List<DailyForecastItem>;
        set => SetValue(DailyForecastDp, value);
    }
}

