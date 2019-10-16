using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseMouseCheese
{
    public static class GridHelper
    {
        static int width = ConfigConstant.GetInt("FRAME_WIDTH");
        static int height = ConfigConstant.GetInt("FRAME_HEIGHT");

        public struct Coord
        {
            public int row;
            public int col;
        }

        public static int GetGridNumber(int row, int col)
        {
            return row * width + col;
        }

        public static Coord GetGridCoord(int index)
        {
            Coord coord;
            coord.row = index / width;
            coord.col = index % width;
            return coord;
        }
    }
}
