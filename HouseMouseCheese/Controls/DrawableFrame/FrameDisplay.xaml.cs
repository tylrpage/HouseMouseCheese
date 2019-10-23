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
    /// Interaction logic for FrameDisplay.xaml
    /// </summary>
    public partial class FrameDisplay : UserControl
    {
        private const int PIXEL_SIZE = 20;

        private Frame _frame;
        public Frame Frame
        {
            get { return _frame; }
            set { _frame = value; Update(); }
        }
        // Pointers to the rectangle (pixel) controls
        private DrawablePixel[] pixels;
        private int _width, _height; // in pixel count
        public FrameDisplay()
        {
            InitializeComponent();
            SizeChanged += FrameDisplay_SizeChanged;

            _frame = new Frame(0);

            _width = ConfigConstant.GetInt("FRAME_WIDTH");
            _height = ConfigConstant.GetInt("FRAME_HEIGHT");

            Dad.Columns = _width;
            Dad.Rows = _height;

            pixels = new DrawablePixel[_width * _height];

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    DrawablePixel pixel = new DrawablePixel(this, GridHelper.GetGridNumber(i, j));
                    pixel.SetValue(Grid.RowProperty, i);
                    pixel.SetValue(Grid.ColumnProperty, j);
                    Dad.Children.Add(pixel);

                    pixels[GridHelper.GetGridNumber(i, j)] = pixel;
                } 
            }
        }

        public void ColorPixel(int pixelIndex)
        {
            Color color = MainWindow.SelectedColor;
            _frame.GetPixel(pixelIndex).Color = color;
            pixels[pixelIndex].Color = color;
        }

        // Adjust the sizes of the pixels to be as large as they can while still maintaining the correct ratio
        private void FrameDisplay_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWidth = e.NewSize.Width;
            double newHeight = FrameRowDefinition.ActualHeight;

            double ratio = _width / _height;
            double actualRatio = newWidth / newHeight;

            if (ratio < actualRatio)
            {
                Dad.Width = ratio * newHeight;
                Dad.Height = newHeight;
            }
            else
            {
                Dad.Width = newWidth;
                Dad.Height = newWidth / ratio;
            }
        }

        void buttonDownHandler(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(this);
            int index = GridHelper.GetGridNumber((int)clickPosition.Y / PIXEL_SIZE, (int)clickPosition.X / PIXEL_SIZE);

            Frame.GetPixel(index).Color = Colors.Red;
            Update();
        }

        // Refresh the display according to the Frame property
        public void Update()
        {
            NumberDisplay.Text = $"Frame: {_frame.Number + 1}";

            int width = ConfigConstant.GetInt("FRAME_WIDTH");
            int height = ConfigConstant.GetInt("FRAME_HEIGHT");

            for (int i = 0; i < width * height; i++)
            {
                pixels[i].Rect.Fill = new SolidColorBrush(_frame.GetPixel(i).Color);
            }
        }
    }
}
