using WeatherApp.Models;

namespace WeatherApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            while (true)
            {
             
                
                Console.Clear();
                string fileName = "../../../Files/tempdata.txt";
                List<string> list = new List<string>() { "Medeltemp/dag", "Medelluftfuktighet/dag", 
                    "Medeltemp/månad", "Lista på alla dagar", "Sortera varmast dagarna", "Sortera torraste dagarna", "Mögelrisk", "Meteorologisk data" };   //En lista med strängar. Bygg en extensions
                list.NumberedList();
                

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                string date;
                string msg;
                char key = keyInfo.KeyChar;

                switch (key)
                {
                    case '1':
                        date = Helper.Date("");
                        AnalyzeNumbers.AverageTemp(date, fileName);
                        Console.ReadKey();

                        break;
                    case '2':
                        date = Helper.Date("");
                        AnalyzeNumbers.AverageHumidity(date, fileName);
                        Console.ReadKey();

                        break;
                    case '3':
                        date = Helper.Month("");
                        AnalyzeNumbers.AverageTemp(date, fileName);
                        Console.ReadKey();
                        break;
                    case '4':
                        HelpersList.WeatherList(fileName);
                        break;
                    case '5':
                        AnalyzeNumbers.WarmestToColdest();
                        break;
                    case '6':
                        AnalyzeNumbers.DryToMoist();
                        break;
                    case '7':
                        AnalyzeNumbers.sortedMoldRisk();
                        break;
                    case '8':
                        AnalyzeNumbers.MeteorologicalAutumn();
                        AnalyzeNumbers.MeterorologicalWinter();
                        break;
                }
            }
            
        }
    }
}
