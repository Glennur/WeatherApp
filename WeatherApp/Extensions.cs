using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{

    internal static class Extensions
    {
        public static void Cw(this string input)     //Ska ta in en sträng, mend valfritt namn. Lägger till this innan
        {
            Console.WriteLine(input);
        }

        public static void NumberedList<T>(this List<T> items)   //En Lista med vad som helst. en generisk metod vilket innebär att man kan skicka in vilken lista som helst med vilken datatyp som helst
        {                                       //I och med this så så kan man lägga till olika metoder. T står för Type. metoden tar in vad som helst. 
            for (int i = 1; i <= items.Count; i++)
            {
                (i + ". " + items[i - 1]).Cw();
            }
        }
    }
}
