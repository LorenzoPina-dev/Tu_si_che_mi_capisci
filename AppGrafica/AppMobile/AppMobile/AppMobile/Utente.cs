using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace proj
{
    class Utente
    {
       string username, mail, immagine, xp;
       public static string key;

        public Utente() { }

        public Utente(string username, string mail, string immagine, string xp)
        {
            this.username = username;
            this.mail = mail;
            this.immagine = immagine;
            this.xp = xp;
        }

        public string Login(string username, string password)
        {
            httpRequests requestLog = new httpRequests();
            key = requestLog.HttpRequestLogin(username, password);
            return key;
        }

        public string Signin(string username, string password, string mail, string img) 
        {
            httpRequests requestSignin = new httpRequests();
            string result = requestSignin.HttpRequestSignin(username, password, mail, img);
            return result;
        }

        public string ForgotPwd(string cookie)
        {
            httpRequests forgotPsw = new httpRequests();
            string result = forgotPsw.HttpRequestForgotPwd(cookie);
            return result;
        }

        public string ResetPwd(string cod, string pass, string pass2)
        {
            httpRequests resetPsw = new httpRequests();
            string result = resetPsw.HttpRequestResetPwd(cod, pass, pass2);
            return result;
        }

        public string ChangeInfo(string user, string pass, string mail, FileBase file)
        {
            httpRequests resetPsw = new httpRequests();
            var ris = resetPsw.HttpRequestChangeInfoAsync(key, user, pass, mail, file).ContinueWith((b) => { if (b.Result.Length > 0) { Console.WriteLine("OK"); } else { Console.WriteLine("Fail"); } }); ;
            return ris.ToString();
        }

        public object GetInfo()
        {
            object obj = "";
            httpRequests getInfo = new httpRequests();
            obj = getInfo.HttpRequestInfo(key);
            return obj;
        }

        public static object GetEmozioni()
        {
            object ris = "";
            httpRequests getEmozioni = new httpRequests();
            ris = getEmozioni.HttpRequestGetEmozioni(key);
            return ris;
        }

        public static object GetEmozioni(string data, int tipo)
        {
            object ris = "";
            httpRequests getEmozioni = new httpRequests();
            ris = getEmozioni.HttpRequestGetEmozioni(key, data, tipo);
            return ris;
        }

        public object GetVoltiTrovati(string max)
        {
            object obj = "";
            httpRequests getVoltiTrovati = new httpRequests();
            obj = getVoltiTrovati.HttpRequestGetVoltiTrovati(key, max);
            return obj;
        }

        public string AddVoltoRegistrato(FileBase file, string nome)
        {
            httpRequests addVoltoRegistrato = new httpRequests();
            var ris = addVoltoRegistrato.HttpRequestAddVoltoRegistratoAsync(key, file, nome).ContinueWith((b) => { if (b.Result.Length > 0) { Console.WriteLine("OK"); } else { Console.WriteLine("Fail"); } }); ; ;
            return ris.ToString();
        }
        public object GetVoltiRegistrati()
        {
            object obj = "";
            httpRequests getVoltiRegistrati = new httpRequests();
            obj = getVoltiRegistrati.HttpRequestGetVoltiRegistrati(key);
            return obj;
        }

        public string DeleteVoltoRegistrato(string id)
        {
            string ris = "";
            httpRequests delVoltoReg = new httpRequests();
            ris = delVoltoReg.HttpRequestDeleteVoltoRegistrato(key, id);
            return ris;
        }

        public string AddDispositivo(string nome, string tipo, string ip)
        {
            string ris = "";
            httpRequests addDispositivo = new httpRequests();
            ris = addDispositivo.HttpRequestAddDispositivo(key, nome, tipo, ip);
            return ris;
        }

        public object GetDispositivi()
        {
            object obj = "";
            httpRequests getDispositivi = new httpRequests();
            obj = getDispositivi.HttpRequestGetDispositivi(key);
            return obj;
        }

        public string DeleteDispositivo(string id)
        {
            string ris = "";
            httpRequests delDispositvo = new httpRequests();
            ris = delDispositvo.HttpRequestDeleteDispositivo(key, id);
            return ris;
        }

        public string AddSkill(string nome, string descrizione, string azione, string idEmozione)
        {
            string ris = "";
            httpRequests addSkill = new httpRequests();
            ris = addSkill.HttpRequestAddSkill(key, nome, descrizione, azione, idEmozione);
            return ris;
        }

        public object GetSkills(string idEmozione)
        {
            object obj = "";
            httpRequests getSkill = new httpRequests();
            obj = getSkill.HttpRequestGetSkills(key, idEmozione);
            return obj;
        }

        public string DeleteSkill(string id)
        {
            string ris = "";
            httpRequests delSkill = new httpRequests();
            ris = delSkill.HttpRequestDeleteSkill(key, id);
            return ris;
        }
        public string AddEmozione(string tipo, string dataRil, string idDisp)
        {
            string ris = "";
            httpRequests addEmozione = new httpRequests();
            ris = addEmozione.HttpRequestAddEmozione(key, tipo, dataRil, idDisp);
            return ris;
        }

        public byte[] GetImage(string tabella, string nome)
        {
            byte[] path;
            httpRequests getImage = new httpRequests();
            path = (byte[])getImage.GetImage(key, tabella, nome);
            return path;
        }
    }
}
