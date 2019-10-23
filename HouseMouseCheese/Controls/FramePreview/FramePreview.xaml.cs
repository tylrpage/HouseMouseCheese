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
    public partial class FramePreview : UserControl
    {
        private const int PIXEL_SIZE = 20;

        private Frame _frame;
        public Frame Frame
        {
            get { return _frame; }
            set { _frame = value; Update(); }
        }
        // Pointers to the rectangle (pixel) controls
        private PixelDisplay[] pixels;
        private int _width, _height;
        public FramePreview()
        {
            InitializeComponent();
            SizeChanged += FrameDisplay_SizeChanged;

            _frame = new Frame();

            _width = ConfigConstant.GetInt("FRAME_WIDTH");
            _height = ConfigConstant.GetInt("FRAME_HEIGHT");

            pixels = new PixelDisplay[_width * _height];

            for (int i = 0; i < _height; i++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                for (int j = 0; j < _width; j++)
                {
                    PixelDisplay pixel = new PixelDisplay(this, _frame.GetPixel(i, j));

                    stackPanel.Children.Add(pixel);
                    pixels[GridHelper.GetGridNumber(i, j)] = pixel;
                }
                Dad.Children.Add(stackPanel);
            }

            Update();
        }

        // Adjust the sizes of the pixels to be as large as they can while still maintaining the correct ratio
        private void FrameDisplay_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double ratio = _width / _height;
            double actualRatio = e.NewSize.Width / e.NewSize.Height;
            double newSize = ratio < actualRatio ? e.NewSize.Height / _height : e.NewSize.Width / _width;
            foreach (PixelDisplay pixel in pixels)
            {
                pixel.Width = newSize;
                pixel.Height = newSize;
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
            int width = ConfigConstant.GetInt("FRAME_WIDTH");
            int height = ConfigConstant.GetInt("FRAME_HEIGHT");

            for (int i = 0; i < width * height; i++)
            {
                pixels[i].Rect.Fill = new SolidColorBrush(Frame.GetPixel(i).Color);
            }
        }
    }
}
