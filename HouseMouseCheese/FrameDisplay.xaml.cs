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
        private Frame _frame;
        public Frame Frame
        {
            get { return _frame; }
            set { _frame = value; Update(); }
        }
        // Pointers to the rectangle (pixel) controls
        private Rectangle[] rectangles;
        public FrameDisplay()
        {
            InitializeComponent();

            int width = ConfigConstant.GetInt("FRAME_WIDTH");
            int height = ConfigConstant.GetInt("FRAME_HEIGHT");

            rectangles = new Rectangle[width * height];

            for (int i = 0; i < height; i++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                for (int j = 0; j < width; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 20;
                    rectangle.Height = 20;

                    stackPanel.Children.Add(rectangle);
                    rectangles[GridHelper.GetGridNumber(i, j)] = rectangle;
                }
                Dad.Children.Add(stackPanel);
            }
        }

        // Refresh the display according to the Frame property
        public void Update()
        {
            int width = ConfigConstant.GetInt("FRAME_WIDTH");
            int height = ConfigConstant.GetInt("FRAME_HEIGHT");

            for (int i = 0; i < width * height; i++)
            {
                rectangles[i].Fill = new SolidColorBrush(Frame.GetPixel(i).Color);
            }
        }
    }
}
