using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class HelpersList
    {
        
        public static List<Models.DailyTemp> WeatherList(string fileName)
        {
            List<Models.DailyTemp> weatherDatas = new List<Models.DailyTemp>();
            string pattern = @"(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<place>Ute|Inne),(?<temp>\d+\.\d+),(?<humidity>\d+)";
            Regex regex = new Regex(pattern);
            using (StreamReader reader = new StreamReader(fileName))
            {
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    Match match = regex.Match(data);
                    if (match.Success)
                    {
                        Models.DailyTemp dailyTemp = new Models.DailyTemp
                        {
                            Date = match.Groups["date"].Value,
                            Time = match.Groups["time"].Value,
                            InsideOutside = match.Groups["place"].Value.Equals("Ute", StringComparison.OrdinalIgnoreCase),

                            Humidity = int.Parse(match.Groups["humidity"].Value)

                        };
                        string tempTemp = match.Groups["temp"].Value.Replace(".", ",");
                        if (decimal.TryParse(tempTemp, out decimal temp))
                        {
                            dailyTemp.Temp = temp;
                        }
                        weatherDatas.Add(dailyTemp);

                    }

                }
               
            }
            return weatherDatas;
        }
    }
}
