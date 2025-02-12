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
                Console.WriteLine("Ange datum: ");
                date = Console.ReadLine();

                dateOk = RegExTester.TestDate(date);
                
            }
            while (!dateOk);
            return date;
        }
        public static string Month(string date)
        {
            bool dateOk = false;
            do
            {
                Console.WriteLine("Ange datum: ");
                date = "2016-" + Console.ReadLine();

                dateOk = RegExTester.TestMonth(date);

            }
            while (!dateOk);
            return date;
        }

    }
}
