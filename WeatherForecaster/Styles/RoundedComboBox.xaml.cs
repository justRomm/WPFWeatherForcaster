using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Media;
using System;
using System.Windows.Input;

namespace WeatherForecaster.Styles
{
    public class RoundedComboBox : ComboBox
    {
        private Thickness _oldThickness = new(15);

        public static readonly DependencyProperty CornerRadiusDp = DependencyProperty.Register("MyCornerRadius", typeof(Thickness), typeof(RoundedComboBox), new PropertyMetadata(new Thickness(15)));

        public Thickness MyCornerRadius
        {
            get => (Thickness)GetValue(CornerRadiusDp);
            set => SetValue(CornerRadiusDp, value);
        }

        public RoundedComboBox() : base()
        {
            DropDownOpened += OnDropDownOpened;
            DropDownClosed += RoundedComboBox_DropDownClosed;
        }

        private void OnDropDownOpened(object sender, EventArgs e)
        {
        }

        private void RoundedComboBox_DropDownClosed(object sender, EventArgs e)
        {
        }
    }
}