using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HouseMouseCheese
{
    public static class ConfigConstant
    {
        public static int GetInt(string key)
        {
            string strValue = ConfigurationManager.AppSettings[key];
            if (strValue == null)
            {
                throw new ArgumentException("Key does not exist in App.config");
            }
            
            return Convert.ToInt32(strValue);
        }
    }
}
