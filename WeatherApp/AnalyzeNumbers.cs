using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherApp.Models;
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


                string monthCheck = @"\d{4}[-](?<month>0[1-9]|1[0-2])?[0-9]?[0-9]?";
                Regex regex1 = new Regex(monthCheck);
                Match matchMonth = regex1.Match(date);
                string monthName;

                if (matchMonth.Success)
                {
                    string testmonth = matchMonth.Groups["month"].Value;
                    if (int.TryParse(testmonth, out int month))
                    {
                        Console.WriteLine(typeof(Models.Enums.Months).GetEnumName(month));
                    }
                }



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
                    Console.WriteLine($"{outsideAvgTemp / outsideRowCount:F2} grader är medeltemperatur utomhus för {date} ");
                    Console.WriteLine($"{insideAvgTemp / insideRowCount:F2} grader är medeltemperatur inomhus för {date}");
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
            List<Models.DailyTemp> warmest = HelpersList.WeatherList("../../../Files/tempdata.txt");


            var sortedWarmest = warmest.GroupBy(x => new { x.Date, x.InsideOutside })

                                       .Select(g => new { groupedDate = g.Key.Date, InsideOutSide = g.Key.InsideOutside, avarageTemp = g.Average(x => x.Temp) })
                                       .OrderByDescending(d => d.avarageTemp).ToList();

            var warmestInside = sortedWarmest
                                .Where(k => !k.InsideOutSide).ToList();

            var warmestOutside = sortedWarmest
                                 .Where(k => k.InsideOutSide).ToList();



            for (int i = 0; i < 10; i++)
            {
                var g = warmestInside[i];

                Console.WriteLine($"{g.groupedDate} {g.avarageTemp:F2} {(g.InsideOutSide)}");

            }
            for (int i = 0; i < 10; i++)
            {
                var g = warmestOutside[i];

                Console.WriteLine($"{g.groupedDate} {g.avarageTemp:F2} {(g.InsideOutSide)}");

            }

            Console.ReadKey(true);

        }
        public static void DryToMoist()
        {
            List<Models.DailyTemp> dryest = HelpersList.WeatherList("../../../Files/tempdata.txt");


            var sortedHumidity = dryest.GroupBy(x => new { x.Date, x.InsideOutside })
                            .Select(g => new
                            {
                                groupedDate = g.Key.Date,
                                InsideOutSide = g.Key.InsideOutside,
                                averageHumidity = g.Average(x => x.Humidity)
                            })
                            .OrderBy(d => d.averageHumidity) // Sorterar i stigande ordning (torrast först)
                            .ToList();

            var driestInside = sortedHumidity.Where(k => !k.InsideOutSide).Take(10).ToList();
            var driestOutside = sortedHumidity.Where(k => k.InsideOutSide).Take(10).ToList();

            var mostHumidInside = sortedHumidity.Where(k => !k.InsideOutSide).OrderByDescending(d => d.averageHumidity).Take(10).ToList();
            var mostHumidOutside = sortedHumidity.Where(k => k.InsideOutSide).OrderByDescending(d => d.averageHumidity).Take(10).ToList();

            Console.WriteLine("Torrast Inomhus:");
            foreach (var g in driestInside)
            {
                Console.WriteLine($"{g.groupedDate} {g.averageHumidity:F2} {(g.InsideOutSide)}");
            }

            Console.WriteLine("Torrast Utomhus:");
            foreach (var g in driestOutside)
            {
                Console.WriteLine($"{g.groupedDate} {g.averageHumidity:F2} {(g.InsideOutSide)}");
            }

            Console.WriteLine("Fuktigast Inomhus:");
            foreach (var g in mostHumidInside)
            {
                Console.WriteLine($"{g.groupedDate} {g.averageHumidity:F2} {(g.InsideOutSide)}");
            }

            Console.WriteLine("Fuktigast Utomhus:");
            foreach (var g in mostHumidOutside)
            {
                Console.WriteLine($"{g.groupedDate} {g.averageHumidity:F2} {(g.InsideOutSide)}");
            }

            Console.ReadKey(true);

        }
        public static void sortedMoldRisk()
        {
            List<Models.DailyTemp> moldRisk = HelpersList.WeatherList("../../../Files/tempdata.txt");

            var sortedMoldRisk = moldRisk.GroupBy(x => new { x.Date, x.InsideOutside })
                                .Select(g => new
                                {
                                    groupedDate = g.Key.Date,
                                    InsideOutSide = g.Key.InsideOutside,
                                    moldRisk = //g.Average(x => ((x.Humidity - 78) * (x.Temp / 15)) / 0.22)

                                    ((g.Average(x => (decimal)x.Humidity) - 78m) *  //Luftfuktigheten minskar med 78. Temperaturen delas med 15. 0.22 förstorar temperaturen 4 ggr.
                                          (g.Average(x => x.Temp) / 15m)) / 0.22m
                                })
                            .OrderBy(d => d.moldRisk)
                            .ToList();

            var lowestMoldRiskInside = sortedMoldRisk.Where(k => !k.InsideOutSide).Take(3).ToList();
            var lowestMoldRiskOutside = sortedMoldRisk.Where(k => k.InsideOutSide).Take(3).ToList();

            var mostMoldRiskInside = sortedMoldRisk.Where(k => !k.InsideOutSide).OrderByDescending(d => d.moldRisk).Take(3).ToList();
            var mostMoldRiskOutside = sortedMoldRisk.Where(k => k.InsideOutSide).OrderByDescending(d => d.moldRisk).Take(3).ToList();

            Console.WriteLine("Minst risk för mogel inomhus:");
            foreach (var g in lowestMoldRiskInside)
            {
                Console.WriteLine($"{g.groupedDate} {g.moldRisk:F2}");
            }

            Console.WriteLine("Minst risk för mögel utomhus:");
            foreach (var g in lowestMoldRiskOutside)
            {
                Console.WriteLine($"{g.groupedDate} {g.moldRisk:F2}");
            }

            Console.WriteLine("Mest risk för mögel inomhus:");
            foreach (var g in mostMoldRiskInside)
            {
                Console.WriteLine($"{g.groupedDate} {g.moldRisk:F2}");
            }

            Console.WriteLine("Minst risk för mogel Utomhus:");
            foreach (var g in mostMoldRiskOutside)
            {
                Console.WriteLine($"{g.groupedDate} {g.moldRisk:F2}");
            }
            Console.ReadKey();
        }
    }
}
