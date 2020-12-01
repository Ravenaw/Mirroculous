using System;

namespace Mirroculous.Model
{
    public class Mirror
    {
        /// <summary>
        /// Properties 
        /// </summary>
        public int ID { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public DateTime DateTime { get; set; }



        /// <summary>
        /// constructors 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="temperature"></param>
        /// <param name="humidity"></param>
        /// <param name="dateTime"></param>
        public Mirror(int id, int temperature, int humidity, DateTime dateTime)
        {
            ID = id;
            Temperature = temperature;
            Humidity = humidity;
            DateTime = dateTime;
        }
        /// <summary>
        /// default constructor
        /// </summary>
        public Mirror()
        {
        }

        /// <summary>
        /// Override to String method - in case we need it
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID:{ID}, Temperature: {Temperature}, Humidity: {Humidity}, Date: {DateTime.ToString("f")}";
        }
    }
}
