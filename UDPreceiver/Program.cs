using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPreceiver
{
    class PrintFormat
    {
        public double temperature { get; set; }
        public double humidity { get; set; }
        public DateTime timestamp { get; set; }

        public PrintFormat(double temp, double hmdt, DateTime stamp)
        {
            temperature = temp;
            humidity = hmdt;
            timestamp = stamp;
        }

        public override string ToString()
        {
            return temperature.ToString("#.#") + "°C\t" + humidity.ToString("#.#") + "%\t" + timestamp;
        }
    }
    class Program
    {
        private const int Port = 55555;
        static void Main(string[] args)
        {
            using (UdpClient socket = new UdpClient(new IPEndPoint(IPAddress.Any, Port)))
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(0, 0);
                while (true)
                {
                    byte[] datagramReceived = socket.Receive(ref remoteEndPoint);
                    string message = Encoding.ASCII.GetString(datagramReceived, 0, datagramReceived.Length);
                    PrintFormat pf = new PrintFormat(double.Parse(message.Split(' ')[0]), double.Parse(message.Split(' ')[1]), DateTime.Parse(message.Split(' ')[2] + " " + message.Split(' ')[3]));
                    Console.WriteLine(pf.ToString());
                }
            }
        }
    }
}
