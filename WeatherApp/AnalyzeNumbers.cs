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
        public static void AverageTemp(string date, string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
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
            using (StreamReader reader = new StreamReader(fileName))
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

        public static void WarmestToColdest()
        {
            
            //var sortedWarmest = ???.OrderByDescending(d => d.AvgTemp).ToList();
        }
        
        
    }
}
