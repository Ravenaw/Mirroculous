﻿using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mirroculous.Model;
using Newtonsoft.Json;

namespace UDPreceiver
{
    class Program
    {
        private const int Port = 55555;

        static void Main(string[] args)
        {
            //Creates a UdpClient for reading incoming data.

            UdpClient udpReceiver = new UdpClient(Port);

            //This IPEndPoint will allow you to read datagrams sent from a specific ip-source on port 7000
            // 192.168.3.224/127.0.0.1 
            // IPAddress ip = IPAddress.Parse("127.0.0.1");
            // IPEndPoint RemoteIpEndPoint = new IPEndPoint(ip, 7000);


            //BROADCASTING RECEIVER
            //This IPEndPoint will allow you to read datagrams sent from any ip-source on port 7000
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, Port);

            //IPEndPoint object will allow us to read datagrams sent from any source.
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // Blocks until a message returns on this socket from a remote host.
            Console.WriteLine("Receiver is blocked");
            try
            {
                while (true)
                {
                    Byte[] receiveBytes = udpReceiver.Receive(ref RemoteIpEndPoint);

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);

                    Console.WriteLine("Sender: " + receivedData.ToString());

                    string[] textLines = receivedData.Split(',');

                    string[] ID_Arr = textLines[0].Split(':');
                    int ID = Convert.ToInt32(ID_Arr[1].Trim());
                    string[] Temp_Arr = textLines[1].Split(':');
                    int Temp = Convert.ToInt32(Temp_Arr[1].Trim());
                    string[] Hum_Arr = textLines[2].Split(':');
                    int Hum = Convert.ToInt32(Hum_Arr[1].Trim());
                    string[] Date_Arr = textLines[3].Split(':');
                    DateTime Date = Convert.ToDateTime((Date_Arr[1]+":"+Date_Arr[2]).Trim());

                    Mirror r_Mirror = new Mirror(ID,Temp,Hum,Date);

                    Post(r_Mirror).Wait();

                    Thread.Sleep(200);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        private static async Task Post(Mirror pf)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PostAsJsonAsync("https://mirroculousweb.azurewebsites.net/mirror", pf);
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
