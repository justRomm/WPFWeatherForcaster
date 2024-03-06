using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WeatherForecaster.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var inputValue = System.Convert.ToBoolean(value);

            if (parameter != null && parameter.ToString() == "invert")
            {
                return inputValue ? Visibility.Collapsed : Visibility.Visible;
            }

            return inputValue ? Visibility.Visible : Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"BooleanToInvisibilityConverter.Convert Error:{ex.Message}");
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}