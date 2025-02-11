using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class RegExTester
    {
        public static bool TestDate(string date)
        {
            string testDate = @"^2016-(?<month>0[1-9]|1[0-2])-(?<day>0[1-9]|[12]\d|3[01])$";
            
            Regex regex = new Regex(testDate);
            Match match = regex.Match(date);

            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
