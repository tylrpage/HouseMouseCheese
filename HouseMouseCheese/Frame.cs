using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseMouseCheese
{
    public class Frame
    {
        // The frame's place in the pattern
        public int Number { get; private set; }
        public Pixel[] Pixels { get; set; }

        public Frame(int number)
        {
            Number = number;
            int width = ConfigConstant.GetInt("FRAME_WIDTH");
            int height = ConfigConstant.GetInt("FRAME_HEIGHT");

            Pixels = new Pixel[width * height];
            for (int i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = new Pixel();
            }
        }

        public Pixel GetPixel(int row, int col)
        {
            return Pixels[GridHelper.GetGridNumber(row, col)];
        }
        public Pixel GetPixel(int position)
        {
            return Pixels[position];
        }
    }
}
