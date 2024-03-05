using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherForecaster.ViewModels
{
    public class CitiesViewModel: DependencyObject
    {
        private static DependencyProperty CitiesListDp = DependencyProperty.Register("CitiesList", typeof(List<string>), typeof(CitiesViewModel));

        public List<string> CitiesList
        {
            get => GetValue(CitiesListDp) as List<string>;
            set => SetValue(CitiesListDp, value);
        }

        private static DependencyProperty IsComboBoxEnabledDp = DependencyProperty.Register("IsComboBoxEnabled", typeof(bool), typeof(CitiesViewModel), new PropertyMetadata(false));

        public bool IsComboBoxEnabled
        {
            get => (GetValue(IsComboBoxEnabledDp) as bool?).Value;
            set => SetValue(IsComboBoxEnabledDp, value);
        }

        private static DependencyProperty IsCitiesTextBlockVisibleDp = DependencyProperty.Register("IsCitiesTextBlockVisible", typeof(bool), typeof(CitiesViewModel), new PropertyMetadata(true));

        public bool IsCitiesTextBlockVisible
        {
            get => (GetValue(IsCitiesTextBlockVisibleDp) as bool?).Value;
            set => SetValue(IsCitiesTextBlockVisibleDp, value);
        }

        private static DependencyProperty CitiesTextBoxTextDp = DependencyProperty.Register("CitiesTextBoxText", typeof(string), typeof(CitiesViewModel), new PropertyMetadata("Enter your city"));

        public string CitiesTextBoxText
        {
            get => (GetValue(CitiesTextBoxTextDp) as string);
            set => SetValue(CitiesTextBoxTextDp, value);
        }

        private static DependencyProperty SelectedCityDp = DependencyProperty.Register("SelectedCity", typeof(string), typeof(CitiesViewModel), new PropertyMetadata("London"));

        public string SelectedCity
        {
            get => (GetValue(SelectedCityDp) as string);
            set => SetValue(SelectedCityDp, value);
        }
    }
}
