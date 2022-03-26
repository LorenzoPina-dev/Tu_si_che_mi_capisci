using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerUdpInput
{
    class Program
    {
        public Queue<Messaggio> in,out;
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient(12345);
            IPEndPoint endP = new IPEndPoint(IPAddress.Any, 0);
            while(true)
            {
                string ris=Encoding.ASCII.GetString(client.Receive(ref endP));

            }
        }
        public
    }
}
