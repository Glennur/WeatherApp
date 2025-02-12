namespace WeatherApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                string fileName = "tempdata.txt";
                Console.WriteLine("1. Medeltemp");
                Console.WriteLine("2. Medelluftfuktighet");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                string date;
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

                        break;
                }
            }
            
        }
    }
}
