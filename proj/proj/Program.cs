using Newtonsoft.Json.Linq;
using System;

namespace proj
{
    class Program
    {
        static void Main(string[] args)
        {
            string user = "paperino", password = "bella", password2 = "bella", mail = "chapo2303@gmail.com", img = "R:/Documents/Immagini/Screenshots/img.jpg";

            Utente utente = new Utente();
            //string result = utente.Signin(user, password, password2, mail);
            //Console.WriteLine("REGISTRAZIONE: " + result);

            string key = utente.Login("minnie4", "ok");
            Console.WriteLine("LOGIN: " + key);

            //string codice = utente.ForgotPwd(mail);
            //Console.WriteLine("FORGOTPWD:" + codice);

            //string codicepwd = "913206939";
            //string pass = "nuovapwd2";
            //string pass2 = "nuovapwd2";
            //string ris = utente.ResetPwd(codicepwd, pass, pass2);
            //Console.WriteLine("RESETPWD:" + ris);

            //string user2 = "minnie4";
            //string pass3 = "ok";
            //string mail2 = "chapo2303@gmail.com";
            //string img2 = "C:/ciao/Brent.jpg";
            //string ris6 = utente.ChangeInfo(key, user2, pass3, mail2, img2);
            //Console.WriteLine("CHANGE INFO: " + ris6);

            //object obj = utente.GetInfo(key);
            //if (obj.GetType() == typeof(string))
            //{
            //    string ris2 = obj.ToString();
            //    Console.WriteLine("GET INFO: " + ris2);
            //}
            //else
            //{
            //    string[] array = (string[])obj;
            //    string username = array[0];
            //    string email = array[1];
            //    string image = array[2];
            //    string xp = array[3];
            //    Console.WriteLine("GET INFO: " + username);
            //    Console.WriteLine("username: " + username);
            //    Console.WriteLine("email: " + email);
            //    Console.WriteLine("image: " + image);
            //    Console.WriteLine("xp: " + xp);
            //    string risImage = "";
            //    if (array.Length == 5)
            //    {
            //        risImage = array[4];
            //        Console.WriteLine("immagine: " + risImage);
            //    }
            //}

            //string r = utente.AddDispositivo(key, "disp2", "1", "172.165.1.1", "1");
            //Console.WriteLine("ADD DISPOSITIVO: " + r);

            //string max = "2";
            //Object r2 = utente.GetDispositivi(key, max);


            //string dataRil = "2022-03-23", oraRil = "08:29", idDisp = "2", idVolto = "4", tipo = "1";
            //string start = "0", numero = "20", tipoe = "1";
            ////string ris = utente.AddEmozione(key, tipo, dataRil, oraRil, idDisp);
            ////Console.WriteLine(ris);

            //string max = "1";
            //utente.GetEmozioni(key, max);



            //utente.AddVoltoRegistrato(key, img, "gianni");

            //string dataRil = "2022-03-24", oraRil = "08:29", idDisp = "2", idVolto = "4"; 
            //string ris = utente.AddVoltoTrovato(key, img, dataRil, oraRil, idDisp, idVolto);
            //Console.WriteLine(ris);
        }
    }
}
