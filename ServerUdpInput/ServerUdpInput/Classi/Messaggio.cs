using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerUdpInput.Classi
{
    public class Messaggio
    {
            public string Ip { get; set; }
            public int porta { get; set; }
            
            public string Testo { get; set; }

        public Messaggio()
        {
        }
        public Messaggio(string riga)
            {
            }

            public Messaggio(string Scelta, string dati)
            {
            }
    }
}
