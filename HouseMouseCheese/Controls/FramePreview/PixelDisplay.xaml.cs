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
    public partial class PixelDisplay : UserControl
    {
        private FramePreview _parent;
        private Pixel _pixel;

        public PixelDisplay(FramePreview parent, Pixel pixel)
        {
            InitializeComponent();
            _parent = parent;
            _pixel = pixel;
        }
    }
}
