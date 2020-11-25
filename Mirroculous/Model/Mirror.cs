using System;

namespace Mirroculous.Model
{
    public class Mirror
    {
        public int ID { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public DateTime DateTime { get; set; }

        public Mirror(int id, int temperature, int humidity, DateTime dateTime)
        {
            ID = id;
            Temperature = temperature;
            Humidity = humidity;
            DateTime = dateTime;
        }

        public Mirror()
        {
        }

        public override string ToString()
        {
            return $"ID:{ID}, Temperature: {Temperature}, Humidity: {Humidity}, Date: {DateTime.ToString("f")}";
        }
    }
}
