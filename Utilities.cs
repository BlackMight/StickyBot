using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO; 

namespace StickyBot
{
    class Utilities
    {
        private static Dictionary<string, string> alertsone; 

        static Utilities()
        {
            string json = File.ReadAllText("SystemLand/alertsone.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alertsone = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetAlert(string key)
        {
            if (alertsone.ContainsKey(key))
            {
                return alertsone[key];
            }
            return "";
        }

        public static string GetFormattedAlert(string key, params object[] parameter)
        {
            if (alertsone.ContainsKey(key))
            {
                return String.Format(alertsone[key], parameter);
            }
            return "";
        }

        public static string GetFormattedAlert(string key, object parameter)
        {
           return GetFormattedAlert(key, new object[] { parameter });
        }

    }
}
