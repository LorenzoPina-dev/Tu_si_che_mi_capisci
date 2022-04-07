using proj;
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

        string username, immagine, xp; 
        public string key { get; set; }
        public utente() { }

        public utente(string username, string mail, string immagine, string xp)
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

        public string Signin(string username, string password, string mail)
        {
            httpRequests requestSignin = new httpRequests();
            string result = requestSignin.HttpRequestSignin(username,password, password, mail);
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

        public string ChangeInfo(string key, string user, string pass, string mail, string img)
        {
            httpRequests resetPsw = new httpRequests();
            string ris = resetPsw.HttpRequestChangeInfoAsync(key, user, pass, mail, img).Result;
            return ris;
        }

        public object GetInfo(string key)
        {
            object obj = "";
            httpRequests getInfo = new httpRequests();
            obj = getInfo.HttpRequestInfo(key);
            return obj;
        }

        public string AddEmozione(string key, string tipo, string dataRil, string oraRil, string idDisp)
        {
            string ris = "";
            httpRequests addEmozione = new httpRequests();
            ris = addEmozione.HttpRequestAddEmozione(key, tipo, dataRil, oraRil, idDisp);
            return ris;
        }

        public object GetEmozioni(string key, string start, string numero, string tipo, string data)
        {
            object ris = "";
            httpRequests getEmozioni = new httpRequests();
            ris = getEmozioni.HttpRequestGetEmozioni(key, start, numero, tipo, data);
            return ris;
        }

        public string AddVoltoTrovato(string key, string img, string dataRil, string oraRil, string idDisp, string idVolto)
        {
            string ris = "";
            httpRequests addVoltoTrovato = new httpRequests();
            ris = addVoltoTrovato.HttpRequestAddVoltoTrovato(key, img, dataRil, oraRil, idDisp, idVolto);
            return ris;
        }
        public object GetVoltiTrovati(string key, string start, string numero, string data)
        {
            object obj = "";
            httpRequests getVoltiTrovati = new httpRequests();
            obj = getVoltiTrovati.HttpRequestGetVoltiTrovati(key, start, numero, data);
            return obj;
        }

        public string AddVoltoRegistrato(string key, string img, string nome)
        {
            string ris = "";
            httpRequests addVoltoRegistrato = new httpRequests();
            ris = addVoltoRegistrato.HttpRequestAddVoltoRegistrato(key, img, nome);
            return ris;
        }
        public object GetVoltiRegistrati(string key, string start, string numero, string data)
        {
            object obj = "";
            httpRequests getVoltiRegistrati = new httpRequests();
            obj = getVoltiRegistrati.HttpRequestGetVoltiRegistrati(key, start, numero, data);
            return obj;
        }

        public string DeleteVoltoRegistrato(string key, string id)
        {
            string ris = "";
            httpRequests delVoltoReg = new httpRequests();
            ris = delVoltoReg.HttpRequestDeleteVoltoRegistrato(key, id);
            return ris;
        }

        public string AddDispositivo(string key, string nome, string tipo, string ip, string acceso)
        {
            string ris = "";
            httpRequests addDispositivo = new httpRequests();
            ris = addDispositivo.HttpRequestAddDispositivo(key, nome, tipo, ip, acceso);
            return ris;
        }

        public object GetDispositivi(string key, string max)
        {
            object obj = "";
            httpRequests getDispositivi = new httpRequests();
            obj = getDispositivi.HttpRequestGetDispositivi(key, max);
            return obj;
        }

        public string DeleteDispositivo(string key, string id)
        {
            string ris = "";
            httpRequests delDispositvo = new httpRequests();
            ris = delDispositvo.HttpRequestDeleteDispositivo(key, id);
            return ris;
        }

        public string AddSkill(string key, string nome, string descrizione, string azione, string idEmozione)
        {
            string ris = "";
            httpRequests addSkill = new httpRequests();
            ris = addSkill.HttpRequestAddSkill(key, nome, descrizione, azione, idEmozione);
            return ris;
        }

        public object GetSkills(string key, string start, string numero, string data)
        {
            object obj = "";
            httpRequests getSkill = new httpRequests();
            obj = getSkill.HttpRequestGetSkills(key, start, numero, data);
            return obj;
        }

        public string DeleteSkill(string key, string id)
        {
            string ris = "";
            httpRequests delSkill = new httpRequests();
            ris = delSkill.HttpRequestDeleteSkill(key, id);
            return ris;
        }



    }
}
