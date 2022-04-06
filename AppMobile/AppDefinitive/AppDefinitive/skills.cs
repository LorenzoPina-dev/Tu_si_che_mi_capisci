using System;
using System.Collections.Generic;
using System.Text;

namespace AppDefinitive
{

    
    public class skills
    {
        public string nome { get; set; }
        public string descrizione { get; set; }
        public string  tipo {get; set;}
        public string azione { get; set; }

        public skills() {
            nome = "";
            descrizione = "";
            tipo = "";
            azione = ""; 
        }
}
}
