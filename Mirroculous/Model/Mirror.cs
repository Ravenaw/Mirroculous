using System;

namespace Mirroculous.Model
{
    public class Mirror
    {
        /// <summary>
        /// The Id of the mirror 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The temperature
        /// </summary>
        public int Temperature { get; set; }
        /// <summary>
        /// Humidity
        /// </summary>
        public int Humidity { get; set; }
        /// <summary>
        /// The date of recording
        /// </summary>
        public DateTime DateTime { get; set; }



        /// <summary>
        /// Default constructor with initialization
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
        /// Empty constructor
        /// </summary>
        public Mirror()
        {
        }

        /// <summary>
        /// Returns the data in a string format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID:{ID}, Temperature:{Temperature}, Humidity:{Humidity}, Date:{DateTime.ToString("f")}";
        }
    }
}
