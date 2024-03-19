using System;
using System.Collections.Generic;
using System.Windows;

namespace WeatherForecaster.ViewModels;

public class CitiesViewModel : DependencyObject
{
    public event Action SelectedCityChanged;

    private static readonly DependencyProperty CitiesListDp =
        DependencyProperty.Register("CitiesList", typeof(List<string>), typeof(CitiesViewModel));

    private static readonly DependencyProperty IsComboBoxEnabledDp = DependencyProperty.Register("IsComboBoxEnabled",
        typeof(bool), typeof(CitiesViewModel), new PropertyMetadata(false));

    private static readonly DependencyProperty IsCitiesTextBlockVisibleDp =
        DependencyProperty.Register("IsCitiesTextBlockVisible", typeof(bool), typeof(CitiesViewModel),
            new PropertyMetadata(true));

    private static readonly DependencyProperty CitiesTextBoxTextDp = DependencyProperty.Register("CitiesTextBoxText",
        typeof(string), typeof(CitiesViewModel), new PropertyMetadata("Enter your city"));

    private static readonly DependencyProperty SelectedCityDp = DependencyProperty.Register("SelectedCity",
        typeof(string), typeof(CitiesViewModel), new PropertyMetadata("Kyiv"));

    public List<string> CitiesList
    {
        get => GetValue(CitiesListDp) as List<string>;
        set => SetValue(CitiesListDp, value);
    }

    public bool IsComboBoxEnabled
    {
        get => (GetValue(IsComboBoxEnabledDp) as bool?).Value;
        set => SetValue(IsComboBoxEnabledDp, value);
    }

    public bool IsCitiesTextBlockVisible
    {
        get => (GetValue(IsCitiesTextBlockVisibleDp) as bool?).Value;
        set => SetValue(IsCitiesTextBlockVisibleDp, value);
    }

    public string CitiesTextBoxText
    {
        get => GetValue(CitiesTextBoxTextDp) as string;
        set => SetValue(CitiesTextBoxTextDp, value);
    }

    public string SelectedCity
    {
        get => GetValue(SelectedCityDp) as string;
        set
        {
            SetValue(SelectedCityDp, value);
            SelectedCityChanged?.Invoke();
        }
    }
}