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
    /// Interaction logic for PixelDisplay.xaml
    /// </summary>
    public partial class DrawablePixel : UserControl
    {
        private const double BORDER_THICKNESS_RATIO = 0.1;

        private Color _color;
        public Color Color { get { return _color; } set { _color = value; Rect.Fill = new SolidColorBrush(value); } }
        private FrameDisplay _parent;
        private int _pixelIndex;

        public DrawablePixel(FrameDisplay parent, int pixelIndex)
        {
            InitializeComponent();

            _parent = parent;
            _pixelIndex = pixelIndex;
            HideBorder();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            // Eye dropper
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                ((MainWindow)Application.Current.MainWindow).ColorPicker.SelectedColor = _color;
            }
            else
            {
                FillSquare(e);
            }
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            ShowBorder();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                FillSquare(e);
            }   
        }

        private void FillSquare(MouseEventArgs e)
        {
            _parent.ColorPixel(_pixelIndex);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            HideBorder();
        }

        private void HideBorder()
        {
            Panel.SetZIndex(this, 0);
            Border.BorderThickness = new Thickness(2);
            Border.BorderBrush = Brushes.LightGray;
        }
        private void ShowBorder()
        {
            Panel.SetZIndex(this, 1);
            Border.BorderThickness = new Thickness(2);
            Border.BorderBrush = Brushes.Black;
        }
    }
}
