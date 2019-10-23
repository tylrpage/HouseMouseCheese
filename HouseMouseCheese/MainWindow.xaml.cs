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

namespace HouseMouseCheese
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Color SelectedColor;
        public Pattern Pattern { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += MainWindow_KeyDown;

            Pattern = new Pattern();
            FrameDisplay.Frame = Pattern.GetCurrentFrame();

            ColorPicker.SelectedColor = Colors.Red;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Go to next/previous frame
            if (e.Key == Key.Right)
            {
                FrameDisplay.Frame = Pattern.NextFrame();
            }
            if (e.Key == Key.Left)
            {
                FrameDisplay.Frame = Pattern.PreviousFrame();
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue != null)
            {
                SelectedColor = (Color)e.NewValue;
            }
        }
    }
}
