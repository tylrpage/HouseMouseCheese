using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseMouseCheese.Helpers
{
    public class DeepCopy
    {
        public static T[] CreateCopy<T>(ICloneable[] items)
        {
            int length = items.Length;
            T[] copy = new T[length];
            for (int i = 0; i < length; i++)
            {
                copy[i] = (T)items[i].Clone();
            }

            return copy;
        }
    }
}
