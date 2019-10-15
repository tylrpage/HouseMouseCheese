using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseMouseCheese
{
    public class Frame
    {
        private Pixel[] _pixels;

        public Frame()
        {
            int width = ConfigConstant.GetInt("FRAME_WIDTH");
            int height = ConfigConstant.GetInt("FRAME_HEIGHT");

            _pixels = new Pixel[width * height];
        }

        public Pixel GetPixel(int row, int col)
        {
            return _pixels[GridHelper.GetGridNumber(row, col)];
        }
        public Pixel GetPixel(int position)
        {
            return _pixels[position];
        }
    }
}
