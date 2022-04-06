using System;
using System.Collections.Generic;
using System.Text;

namespace AppDefinitive
{
    public class utente
    {
        // magari ogni utente ha un proprio vettore con le skills 
        public string mail { get; set; }
        public string password { get; set; }

        public List<skills> sLista { get; set; }
        public utente() {
            sLista = new List<skills>(); 

            mail = "";
            password = "";
            skills s = new skills();
            s.descrizione = "è solo un aprova, ma funziono"; 
            sLista.Add(s); 
        }


    }
}
