using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HouseMouseCheese
{
    public class Pixel : ICloneable
    {
        public Color Color { get; set; }

        public Pixel(Color color)
        {
            Color = color;
        }

        public Pixel()
        {
            //Color = ColorHelper.GetRandomColor();
            Color = Colors.Transparent;
        }

        public object Clone()
        {
            Pixel clone = (Pixel)this.MemberwiseClone();
            clone.Color = this.Color;
            return clone;
        }
    }
}
