using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UDPreceiver
{
    class Mirror
    {
        public int id { get; set; }
        public int temperature { get; set; }
        public int humidity { get; set; }
        public DateTime timestamp { get; set; }

        public Mirror(int temp, int hmdt, DateTime stamp)
        {
            temperature = temp;
            humidity = hmdt;
            timestamp = stamp;
        }

        public override string ToString()
        {
            return temperature + "°C\t" + humidity + "%\t" + timestamp;
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
                    Mirror pf = new Mirror(Convert.ToInt32(double.Parse(message.Split(' ')[0])),
                        Convert.ToInt32(double.Parse(message.Split(' ')[1])),
                        /*DateTime.Parse(message.Split(' ')[2] + " " + message.Split(' ')[3]))*/DateTime.Now);
                    Console.WriteLine(pf.ToString());
                    Post(pf).Wait();
                }
            }
        }
        private static async Task Post(Mirror pf)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PostAsJsonAsync("https://localhost:44339/mirror", pf);
            }
        }

        /*private static async Task Post2(PrintFormat pf)
        {
            string jsonString = JsonConvert.SerializeObject(pf);
            Console.WriteLine(jsonString);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync("https://localhost:44339/mirror", stringContent);
            }
        }*/
    }
}
