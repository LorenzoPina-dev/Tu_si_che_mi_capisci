using Newtonsoft.Json.Linq;
using System;

namespace proj
{
    class Program
    {
        static void Main(string[] args)
        {
            string user = "pluto", password = "1234", password2 = "1234", mail = "ciao@gmail.com", img = "R:/Documents/Immagini/Screenshots/img.jpg";

            Utente utente = new Utente();
            //string result = utente.Signin(user, password, password2, mail);
            //Console.WriteLine(result);

            string key = utente.Login("gianni", "1234");
            Console.WriteLine(key);

            //string codice = utente.ForgotPwd(mail);
            //Console.WriteLine(codice);

            /*string codice = "5947632";
            string pass = "nuovapwd2";
            string pass2 = "nuovapwd2";
            string ris = utente.ResetPwd(codice, pass, pass2);
            Console.WriteLine(ris);*/

            //string user2 = "gianni";
            //string pass = "1234";
            //string mail2 = "pluto@gmail.com";
            //string img2 = "R:/Documents/Immagini/Screenshots/4k.jpg";
            //string ris6 = utente.ChangeInfo(key, user2, pass, mail2, img2);
            //Console.WriteLine(ris6);

            //object obj = utente.GetInfo(key);
            //if (obj.GetType() == typeof(string))
            //{
            //    string ris = obj.ToString();
            //    Console.WriteLine(ris);
            //}
            //else
            //{
            //    string[] array = (string[])obj;
            //    string username = array[0];
            //    string email = array[1];
            //    string image = array[2];
            //    string xp = array[3];
            //    Console.WriteLine(username);
            //    Console.WriteLine(email);
            //    Console.WriteLine(image);
            //    Console.WriteLine(xp);
            //    string risImage = "";
            //    if (array.Length == 5)
            //    {
            //        risImage = array[4];
            //        Console.WriteLine(risImage);
            //    }
            //}

            //string r = utente.AddDispositivo(key, "disp2", "1", "172.165.1.1", "1");
            //Console.WriteLine(r);

            //string dataRil = "2022-03-23", oraRil = "08:29", idDisp = "2", idVolto = "4", tipo = "1";
            //string start = "0", numero = "20", tipoe = "1";
            ////string ris = utente.AddEmozione(key, tipo, dataRil, oraRil, idDisp);
            ////Console.WriteLine(ris);

            //utente.GetEmozioni(key, start, numero, tipoe, dataRil);



            //utente.AddVoltoRegistrato(key, img, "gianni");

            //string dataRil = "2022-03-24", oraRil = "08:29", idDisp = "2", idVolto = "4"; 
            //string ris = utente.AddVoltoTrovato(key, img, dataRil, oraRil, idDisp, idVolto);
            //Console.WriteLine(ris);
        }
    }
}
