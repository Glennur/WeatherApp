using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class DailyTemp
    {
        public string? Date { get; set; }
        public string? Time { get; set; }
        public decimal Temp { get; set; }
        public bool InsideOutside { get; set; }
        public int Humidity { get; set; }


        //public Test(string date, string time, decimal temp, bool insideOut, int humidity)
        //{
        //    Date = date;
        //    Time = time;
        //    Temp = temp;
        //    InsideOutside = insideOut;
        //    Humidity = humidity;

        //}
        //public override string ToString()
        //{
        //    return $"{Date} {Time}, {(InsideOutside ? "Ute" : "Inne")}, {Temp}°C, {Humidity}%";
        //}
    }
    

}
