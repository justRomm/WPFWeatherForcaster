using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WeatherForecaster.ViewModels;

namespace WeatherForecaster
{
    public partial class MainWindow : Window
    {
        private NavigatingCancelEventArgs _navArgs;
        private CitiesViewModel _cities = new();

        public MainWindow()
        {
            InitializeComponent();

            _cities.CitiesList = cities;


            this.DataContext = new
            {
                cities = _cities
            };

            GoToHome();
        }

        private void header_Loaded(object sender, RoutedEventArgs e)
        {
            InitHeader(sender as Grid);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private StringBuilder SearchInput { get; } = new StringBuilder();
        private const int ThresholdInputTextLength = 3;


        private List<string> cities =
        [
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego",
            "Dallas", "San Jose", "Austin", "Jacksonville"
        ];

        private void CitySearchBoxTextChanged(object sender, EventArgs e)
        {
            string searchTerm = (sender as TextBox).Text.ToLower();

            if (searchTerm.Length < ThresholdInputTextLength)
            {
                _cities.IsComboBoxEnabled = false;
                return;
            }

            List<string> filteredCities = cities
                .Where(city => city.ToLower().Contains(searchTerm))
                .ToList();

            _cities.CitiesList = filteredCities;
            if (filteredCities.Count != 0 && !_cities.IsComboBoxEnabled)
            {
                _cities.IsComboBoxEnabled = true;
            }

            if (filteredCities.Count == 0)
            {
                _cities.IsComboBoxEnabled = false;
            }
        }

        private void OnComboBoxLostFocus(object sender, EventArgs e)
          => this.SearchInput.Clear();

        

        private void GoToHome()
        {
            var home = new Home(_cities);
            NavigationFrame.Navigate(home);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InitHeader(Grid header)
        {
            var restoreIfMove = false;

            header.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ClickCount == 2)
                {
                    if ((ResizeMode == ResizeMode.CanResize) ||
                        (ResizeMode == ResizeMode.CanResizeWithGrip))
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
            header.MouseLeftButtonUp += (s, e) =>
            {
                restoreIfMove = false;
            };
            header.MouseMove += (s, e) =>
            {
                if (restoreIfMove)
                {
                    restoreIfMove = false;
                    var mouseX = e.GetPosition(this).X;
                    var width = RestoreBounds.Width;
                    var x = mouseX - width / 2;

                    if (x < 0)
                    {
                        x = 0;
                    }
                    else
                    if (x + width > SystemParameters.PrimaryScreenWidth)
                    {
                        x = SystemParameters.PrimaryScreenWidth - width;
                    }

                    WindowState = WindowState.Normal;
                    Left = x;
                    Top = 0;
                    DragMove();
                }
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
            if ((e.OriginalSource as ComboBox)?.SelectedItem is not string selectedItem)
            {
                return;
            }

            (e.OriginalSource as ComboBox).SelectedItem = null;

            _cities.CitiesTextBoxText = selectedItem;
        }

        private void CitiesSearchBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (_cities.CitiesTextBoxText == "Enter your city")
            {
                (sender as TextBox).Text = string.Empty;
            }
            _cities.IsCitiesTextBlockVisible = false;
        }

        private void CitySearchBoxOnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as TextBox).Text) && _cities.CitiesTextBoxText == string.Empty )
            {
                _cities.CitiesTextBoxText = "Enter your city";
            }
            
            _cities.IsCitiesTextBlockVisible = true;
        }
    }

}
