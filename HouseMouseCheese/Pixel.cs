using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HouseMouseCheese
{
    public class Pixel
    {
        public Color Color { get; set; }

        public Pixel(Color color)
        {
            Color = color;
        }

        public Pixel()
        {
            //Color = ColorHelper.GetRandomColor();
            Color = Colors.LightGray;
        }
    }
}
