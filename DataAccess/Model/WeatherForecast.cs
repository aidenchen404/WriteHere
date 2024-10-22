using System;

namespace DataAccess.Model
{
    public class WeatherForecast
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
  
    public class LookupE
    {
        public string Text;
        public int Value;
    }
}