using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerUdpInput.Classi;
namespace ServerUdpInput
{
    class Program
    {
        public static bool termina = false;
        static void Main(string[] args)
        {
            DatiCondivisi.Instance().AddMessaggioDaInviare(new Messaggio() { Ip = "80.22.36.186",porta= 12345, Testo= "{'Tipo':audio,'KeyUtente':'cnasjkcnskjl','Dati':'vndsjkvnl'}" });
           new Thread(Ricevi).Start();
            new Thread(Invia).Start();
            new Thread(Elabora).Start();
            new Thread(Elabora).Start();
            /* while (!termina)
            {
                Console.WriteLine("Inserisci exit per terminare il programma");
                termina = Console.ReadLine().ToUpper() == "exit".ToUpper();
            }*/
        }
        public static void Ricevi()
        {
            while(!termina)
            {
                try
                {
                    Messaggio m = GestioneUdp.Instance().Ricevi();
                    DatiCondivisi.Instance().AddMessaggioRicevuti(m);
                    Console.WriteLine(m.Testo);
                }
                catch (Exception) { }
            }
        }
        public static void Invia()
        {
            while (!termina)
            {
                try
                {
                    Messaggio daInviare = DatiCondivisi.Instance().GetMessaggioDaInviare();
                    if (daInviare != null)
                    {
                        GestioneUdp.Instance().Invia(daInviare);
                        Console.WriteLine(daInviare.Testo);
                    }
                }
                catch (Exception) { }
            }
        }
        public static void Elabora()
        {
            while (!termina)
            {
                try
                {
                    Messaggio daElaborare = DatiCondivisi.Instance().GetMessaggioRicevuti();
                    if (daElaborare != null) { 
                        DatiCondivisi.Instance().AddMessaggioDaInviare(DatiCondivisi.Instance().Elabora(daElaborare));
                        Console.WriteLine(daElaborare.Testo);
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
