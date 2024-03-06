using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WeatherForecaster.ViewModels;

namespace WeatherForecaster;

public partial class MainWindow : Window
{
    private const int ThresholdInputTextLength = 3;
    private readonly CitiesViewModel m_cities = new();

    private readonly List<string> cities =
    [
        "New York",
        "Los Angeles",
        "Chicago",
        "Houston",
        "Phoenix",
        "Philadelphia",
        "San Antonio",
        "San Diego",
        "Dallas",
        "San Jose",
        "Austin",
        "Jacksonville"
    ];

    private NavigatingCancelEventArgs m_navArgs;

    public MainWindow()
    {
        InitializeComponent();

        m_cities.CitiesList = cities;

        DataContext = new
        {
            cities = m_cities
        };

        GoToHome();
    }

    private StringBuilder SearchInput { get; } = new();

    private void Header_Loaded(object sender, RoutedEventArgs e)
    {
        InitHeader(sender as Grid);
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void CitySearchBoxTextChanged(object sender, EventArgs e)
    {
        var searchTerm = (sender as TextBox).Text.ToLower();

        if (searchTerm.Length < ThresholdInputTextLength)
        {
            m_cities.IsComboBoxEnabled = false;
            return;
        }

        var filteredCities = cities
            .Where(city => city.ToLower().Contains(searchTerm))
            .ToList();

        m_cities.CitiesList = filteredCities;
        if (filteredCities.Count != 0 && !m_cities.IsComboBoxEnabled)
        {
            m_cities.IsComboBoxEnabled = true;
        }

        if (filteredCities.Count == 0)
        {
            m_cities.IsComboBoxEnabled = false;
        }
    }

    private void OnComboBoxLostFocus(object sender, EventArgs e)
    {
        SearchInput.Clear();
    }

    private void GoToHome()
    {
        var home = new Home(m_cities);
        NavigationFrame.Navigate(home);
    }

    private void button3_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void InitHeader(Grid header)
    {
        var restoreIfMove = false;

        header.MouseLeftButtonDown += (s, e) =>
        {
            if (e.ClickCount == 2)
            {
                if (ResizeMode == ResizeMode.CanResize ||
                    ResizeMode == ResizeMode.CanResizeWithGrip)
                {
                    SwitchState();
                }
            }
            else
            {
                if (WindowState == WindowState.Maximized)
                {
                    restoreIfMove = true;
                }

                DragMove();
            }
        };
        header.MouseLeftButtonUp += (s, e) => { restoreIfMove = false; };
        header.MouseMove += (s, e) =>
        {
            if (!restoreIfMove)
            {
                return;
            }
            restoreIfMove = false;
            var mouseX = e.GetPosition(this).X;
            var width = RestoreBounds.Width;
            var x = mouseX - width / 2;

            if (x < 0)
            {
                x = 0;
            }
            else if (x + width > SystemParameters.PrimaryScreenWidth)
            {
                x = SystemParameters.PrimaryScreenWidth - width;
            }

            WindowState = WindowState.Normal;
            Left = x;
            Top = 0;
            DragMove();
        };
    }

    private void SwitchState()
    {
        switch (WindowState)
        {
            case WindowState.Normal:
            {
                WindowState = WindowState.Maximized;
                break;
            }
            case WindowState.Maximized:
            {
                WindowState = WindowState.Normal;
                break;
            }
        }
    }

    private void CitiesComboBoxSelectionChanged(object sender, RoutedEventArgs e)
    {
        var send = (e.OriginalSource as ComboBox);

        if (send?.SelectedItem is not string selectedItem)
        {
            return;
        }

        send.SelectedItem = null;

        send.IsDropDownOpen = false;

        m_cities.CitiesTextBoxText = selectedItem;


        m_cities.SelectedCity = selectedItem;
    }

    private void CitiesSearchBoxGotFocus(object sender, RoutedEventArgs e)
    {
        if (m_cities.CitiesTextBoxText == "Enter your city")
        {
            (sender as TextBox).Text = string.Empty;
        }
        m_cities.IsCitiesTextBlockVisible = false;
    }

    private void CitySearchBoxOnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (string.IsNullOrEmpty((sender as TextBox).Text) && m_cities.CitiesTextBoxText == string.Empty)
        {
            m_cities.CitiesTextBoxText = "Enter your city";
        }

        m_cities.IsCitiesTextBlockVisible = true;
    }
}