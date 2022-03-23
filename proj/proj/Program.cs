using Newtonsoft.Json.Linq;
using System;

namespace proj
{
    class Program
    {
        static void Main(string[] args)
        {
            string user = "pluto", password = "1234", password2 = "1234", mail = "ciao@gmail.com", img = "R:/Documents/Immagini/Screenshots/img.jpg";

            Utente utente = new Utente(user, password, mail, img);
            //string result = utente.Signin(user, password, password2, mail);
            //Console.WriteLine(result);

            string key = utente.Login("pluto", "nuovapwd");
            Console.WriteLine(key);

            //string codice = utente.ForgotPwd(mail);
            //Console.WriteLine(codice);

            //string codice = "0417126";
            //string pass = "nuovapwd";
            //string pass2 = "nuovapwd";
            //string ris = utente.ResetPwd(codice, pass, pass2);
            //Console.WriteLine(ris);

            //string user2 = "pluto";
            //string pass = "5678";
            //string mail2 = "buongiorno@gmail.com";
            //string img2 = "R:/Documents/Immagini/Screenshots/4k.jpg";
            //string ris = utente.ChangeInfo(key, user2, pass, mail2, img2);
            //Console.WriteLine(ris);

            object obj = utente.GetInfo(key);
            string ris = "", username = "", xp = "", email = "", image = "";
            if (obj.GetType() == typeof(string))
            {
                ris = obj.ToString();
                Console.WriteLine(ris);
            }
            else
            {
                string[] array = (string[])obj;
                username = array[0];
                email = array[1];
                image = array[2];
                xp = array[2];
                Console.WriteLine(username);
                Console.WriteLine(email);
                Console.WriteLine(image);
                Console.WriteLine(xp);
            }



        }
    }
}
