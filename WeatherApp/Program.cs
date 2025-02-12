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
                //List.WeatherList(fileName);
                Console.WriteLine("1. Medeltemp/dag");
                Console.WriteLine("2. Medelluftfuktighet/dag");
                Console.WriteLine("3. Medeltemp/månad");
                Console.WriteLine("4. Lista på alla dagar");

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
                        List.WeatherList(fileName);
                        break;
                }
            }
            
        }
    }
}
