using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class Helper
    {
        public static string Date(string date)
        {
            bool dateOk = false;
            do
            {
                Console.WriteLine("Ange datu m: ");
                date = Console.ReadLine();

                dateOk = RegExTester.TestDate(date);
                
            }
            while (!dateOk);
            return date;
        }
    }
}
