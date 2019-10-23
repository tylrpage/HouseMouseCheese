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
        private FrameDisplay _parent;
        private Pixel _pixel;

        public DrawablePixel(FrameDisplay parent, Pixel pixel)
        {
            InitializeComponent();
            _parent = parent;
            _pixel = pixel;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            FillSquare(e);
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            FillSquare(e);
        }

        private void FillSquare(MouseEventArgs e)
        {
            ShowBorder();

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _pixel.Color = MainWindow.SelectedColor;
                _parent.Update();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            HideBorder();
        }

        private void HideBorder()
        {
            Margin = new Thickness(0);
            Border.BorderThickness = new Thickness(0);
        }
        private void ShowBorder()
        {
            Border.BorderThickness = new Thickness(2);
        }
    }
}
