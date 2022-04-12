using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerUdpInput.Classi
{
    class GestioneUdp
    {
        private UdpClient client;
        private static GestioneUdp instance = null;

        private GestioneUdp()
        {
            client = new UdpClient(65432);
            client.Client.ReceiveTimeout = 5000;
        }
        public static GestioneUdp Instance()
        {
            if (instance == null)
                instance = new GestioneUdp();
            return instance;
        }
        public  void Invia(Messaggio m)
        {
            byte[] send = Encoding.ASCII.GetBytes(m.Testo);
            client.Send(send, send.Length,m.Ip,m.porta);
        }
        public  Messaggio Ricevi()
        {
            IPEndPoint porta = new IPEndPoint(IPAddress.Loopback, 0);
            byte[] ricevuto = client.Receive(ref porta);
            return new Messaggio(Encoding.ASCII.GetString(ricevuto));
        }
    }
}
