using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherForcaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigatingCancelEventArgs _navArgs;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void header_Loaded(object sender, RoutedEventArgs e)
        {
            InitHeader(sender as Grid);
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
    }

}
