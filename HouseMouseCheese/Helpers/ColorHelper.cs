using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HouseMouseCheese
{
    public static class ColorHelper
    {
        private static Random random = new Random();
        public static Color GetRandomColor()
        {
            return Color.FromRgb
                (
                    Convert.ToByte(random.Next(256)), 
                    Convert.ToByte(random.Next(256)), 
                    Convert.ToByte(random.Next(256))
                );
        }
    }
}
