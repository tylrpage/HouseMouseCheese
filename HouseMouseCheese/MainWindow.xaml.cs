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
using HouseMouseCheese.Helpers;
using Microsoft.Win32;

namespace HouseMouseCheese
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Color SelectedColor;
        public Pattern Pattern { get; set; }
        private Pixel[] _clipBoard = null;

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
            if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                _clipBoard = DeepCopy.CreateCopy<Pixel>(FrameDisplay.Frame.Pixels);
            }
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (_clipBoard != null)
                {
                    FrameDisplay.Frame.Pixels = DeepCopy.CreateCopy<Pixel>(_clipBoard);
                    FrameDisplay.Update();
                }
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            // Don't allow the color picker to hold any keyboard focus so that arrow keys work
            Keyboard.Focus(ClearButton);

            if (e.NewValue != null)
            {
                SelectedColor = (Color)e.NewValue;
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Cheese file (*.hmc)|*.hmc";
            if (saveFileDialog.ShowDialog() == true)
            {
                PatternSerializer.WritePattern(Pattern, saveFileDialog.FileName);
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Cheese file (*.hmc)|*.hmc";
            if (openFileDialog.ShowDialog() == true)
            {
                Pattern newPattern = PatternSerializer.ReadPattern(openFileDialog.FileName);
                Pattern = newPattern;
                FrameDisplay.Frame = Pattern.Frames[0];
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Pattern = new Pattern();
            FrameDisplay.Frame = Pattern.Frames[0];
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            ArduinoSerialWriter writer = new ArduinoSerialWriter("COM6", 3);
            writer.WritePattern(Pattern);
        }
    }
}
