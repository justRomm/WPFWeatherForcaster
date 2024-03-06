using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WeatherForecaster.Converters;

public class DoubleToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var inputValue = System.Convert.ToDouble(value);

            return $"{Math.Round(inputValue)}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DoubleToStringConverter.Convert Error:{ex.Message}");
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}