using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerUdpInput.Classi
{
    public class DatiCondivisi
    {
        private static DatiCondivisi instance = null;
        private Queue<Messaggio> MRicevuti, MDaInviare;
        private static int idFile = 1;
        private DatiCondivisi()
        {
            MRicevuti = new Queue<Messaggio>();
            MDaInviare = new Queue<Messaggio>();
        }
        public static DatiCondivisi Instance()
        {
            if (instance == null)
                instance = new DatiCondivisi();
            return instance;
        }
        public Messaggio GetMessaggioRicevuti()
        {
            lock(this)
            {
                if (MRicevuti.Count > 0)
                    return MRicevuti.Dequeue();
                return null;
            }
        }
        public Messaggio GetMessaggioDaInviare()
        {
            lock (this)
            {
                if (MDaInviare.Count > 0)
                    return MDaInviare.Dequeue();
                return null;
            }
        }
        public void AddMessaggioRicevuti(Messaggio m)
        {
            lock (this)
            {
                MRicevuti.Enqueue(m);
            }
        }
        public void AddMessaggioDaInviare(Messaggio m)
        {
            lock (this)
            {
                MDaInviare.Enqueue(m);
            }
        }
        public Messaggio Elabora(Messaggio m)
        {
            JObject mess = new JObject(m.Testo);
            if (mess.ContainsKey("Tipo"))
            {
                string tipo = mess["Tipo"].ToString();
                if (tipo.ToUpper() == "audio".ToUpper()||tipo.ToUpper() == "video".ToUpper())
                {
                    string file = "./" + tipo.ToUpper() == "audio".ToUpper() ? "Emozione" : "Volto" + (idFile++) + ".json";
                    while (File.Exists(file)) { Thread.Sleep(30); }
                    StreamWriter sw = new StreamWriter(file);
                    sw.Write("{'KeyUtente':'"+ mess["KeyUtente"].ToString()+"','Dati':'"+ mess["Dati"].ToString()+"'}");
                    sw.Flush();
                    sw.Close();
                    m.Testo = "{'success':true}";
                }
                else
                    m.Testo = "{'success':false,'testo':'tipo errato'}";
            }
            else
                m.Testo = "{'success':false,'testo':'manca il tipo'}";
            return m;
        }
        ~DatiCondivisi()
        {
            StreamWriter sw = new StreamWriter("./indice.txt");
            sw.Write(idFile);
            sw.Flush();
            sw.Close();
        }
    }


}
