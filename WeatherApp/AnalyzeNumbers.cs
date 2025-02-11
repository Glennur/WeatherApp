using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                int rowCount = 0;
                decimal avgTemp = 0;

                string pattern = $@"{date} (?<time>\d+:\d+:\d+),(?<place>Ute|Inne),(?<temp>\d+\.\d)";

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
                            avgTemp += temp;
                            rowCount++;
                            
                        }
                    }
                }
                if (rowCount > 0)
                {
                    Console.WriteLine($"{avgTemp / rowCount} grader är medeltemperatur för {date}");
                }
                else
                {
                    Console.WriteLine("Inga mätningar hittades för valt datum");
                }
                
            }
        
        }
    }
}
