using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherApp
{
    internal class AnalyzeNumbers
    {
        public static string path = "../../../Files/";
        public static void AverageTemp(string date, string fileName)
        {
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                string line;
                int outsideRowCount = 0;
                int insideRowCount = 0;
                decimal outsideAvgTemp = 0;
                decimal insideAvgTemp = 0;

                string pattern = $@"{date}[-]?[0-9]?[0-9]? (?<time>\d+:\d+:\d+),(?<place>Ute|Inne),(?<temp>\d+\.\d)";

                Regex regex = new Regex(pattern);


                while ((line = reader.ReadLine()) != null)

                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        //Console.WriteLine(line);
                        string tempString = match.Groups["temp"].Value.Replace(".", ",");
                        if (decimal.TryParse(tempString, out decimal temp))
                        {
                            if (line.Contains("Ute"))
                            {
                                outsideAvgTemp += temp;
                                outsideRowCount++;
                            }
                            else
                            {
                                insideAvgTemp += temp;
                                insideRowCount++;
                            }
                            
                        }
                    }
                }
                if (outsideRowCount > 0 && insideRowCount > 0)
                {
                    Console.WriteLine($"{outsideAvgTemp / outsideRowCount:F2} grader är medeltemperatur utomhus för {date}");
                    Console.WriteLine($"{insideAvgTemp/insideRowCount:F2} grader är medeltemperatur inomhus för {date}");
                }
                else
                {
                    Console.WriteLine("Inga mätningar hittades för valt datum");
                }
                
            }
        
        }


        public static void AverageHumidity(string date, string fileName)
        {
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                string line;
                int outsideRowCount = 0;
                int insideRowCount = 0;
                decimal outsideHumidity = 0;
                decimal insideHumidity = 0;

                string pattern = $@"{date}[-]?[0-9]?[0-9]? (?<time>\d+:\d+:\d+),(?<place>Ute|Inne),(?<temp>\d+\.\d),(?<humidity>\d+)";

                Regex regex = new Regex(pattern);


                while ((line = reader.ReadLine()) != null)

                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        //Console.WriteLine(line);
                        string tempString = match.Groups["humidity"].Value.Replace(".", ",");
                        if (decimal.TryParse(tempString, out decimal humidity))
                        {
                            if (line.Contains("Ute"))
                            {
                                outsideHumidity += humidity;
                                outsideRowCount++;
                            }
                            else
                            {
                                insideHumidity += humidity;
                                insideRowCount++;
                            }

                        }
                    }
                }
                if (outsideRowCount > 0 && insideRowCount > 0)
                {
                    Console.WriteLine($"Luftfuktigheten är {outsideHumidity / outsideRowCount:F2} utomhus för {date}");
                    Console.WriteLine($"Luftfuktigheten är {insideHumidity / insideRowCount:F2} inomhus för {date}");
                }
                else
                {
                    Console.WriteLine("Inga mätningar hittades för valt datum");
                }

            }

        }
        
        public static void WarmestToColdest(string fileName)
        {
            List<Models.DailyTemp> weatherDatas = new List<Models.DailyTemp>();
            //string pattern = $@"(?<date>2016-(?<month>0[1-9]|1[0-2])-(?<day>0[1-9]|[12]\d|3[01]))\s(?<time>\d+:\d+:\d+),(?<place>Ute|Inne),(?<temp>\d+\.\d),(?<humidity>\d+)";
            string pattern = @"(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<place>Ute|Inne),(?<temp>\d+\.\d+),(?<humidity>\d+)";
            Regex regex = new Regex(pattern);
            using (StreamReader reader = new StreamReader(path + fileName))
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
                foreach (var item in weatherDatas)
                {
                    Console.WriteLine(item.Date + "\t" + item.Time + "\t" + item.Temp + "\t" + item.Humidity);
                }

            }

        }
    }
}
