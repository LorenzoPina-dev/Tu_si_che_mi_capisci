using System;
using System.Collections.Generic;
using System.Text;

namespace proj
{
    class Utente
    {
        string username, password, mail, immagine;

        public Utente() { }

        public Utente(string username, string password, string mail, string immagine)
        {
            this.username = username;
            this.password = password;
            this.mail = mail;
            this.immagine = immagine;
        }

        public string Login(string username, string password)
        {
            httpPostRequest requestLog = new httpPostRequest();
            string key = requestLog.HttpPostRequestLogin(username, password);
            return key;
        }

        public string Signin(string username, string password, string mail, string img) 
        {
            httpPostRequest requestSignin = new httpPostRequest();
            string result = requestSignin.HttpPostRequestSignin(username, password, mail, img);
            return result;
        }

        public string ForgotPwd(string cookie)
        {
            httpPostRequest forgotPsw = new httpPostRequest();
            string result = forgotPsw.HttpGetRequestForgotPwd(cookie);
            return result;
        }

        public string ResetPwd(string cod, string pass, string pass2)
        {
            httpPostRequest resetPsw = new httpPostRequest();
            string result = resetPsw.HttpGetRequestResetPwd(cod, pass, pass2);
            return result;
        }

        public string ChangeInfo(string key, string user, string pass, string mail, string img)
        {
            httpPostRequest resetPsw = new httpPostRequest();
            string ris = resetPsw.HttpGetRequestChangeInfo(key, user, pass, mail, img);
            return ris;
        }

        public object GetInfo(string key)
        {
            object obj = "";
            httpPostRequest getInfo = new httpPostRequest();
            obj = getInfo.HttpGetRequestInfo(key);
            return obj;
        }

        public string AddEmozione(string key, string tipo, string dataRil, string idDisp)
        {
            string ris = "";
            httpPostRequest addEmozione = new httpPostRequest();
            ris = addEmozione.HttpAddEmozione(key, tipo, dataRil, idDisp);
            return ris;
        }

        public object GetEmozioni(string key, string start, string numero, string tipo, string data)
        {
            object ris = "";
            httpPostRequest getEmozioni = new httpPostRequest();
            ris = getEmozioni.HttpGetEmozioni(key, start, numero, tipo, data);
            return ris;
        }

        public string AddVoltoTrovato(string key, string img, string dataRil, string idDisp, string idVolto)
        {
            string ris = "";
            httpPostRequest addVoltoTrovato = new httpPostRequest();
            ris = addVoltoTrovato.HttpAddVoltoTrovato(key, img, dataRil, idDisp, idVolto);
            return ris;
        }
        public object GetVoltiTrovati(string key, string start, string numero, string data)
        {
            object obj = "";
            httpPostRequest getVoltiTrovati = new httpPostRequest();
            obj = getVoltiTrovati.HttpGetVoltiTrovati(key, start, numero, data);
            return obj;
        }

        public string AddVoltoRegistrato(string key, string img, string nome)
        {
            string ris = "";
            httpPostRequest addVoltoRegistrato = new httpPostRequest();
            ris = addVoltoRegistrato.HttpAddVoltoRegistrato(key, img, nome);
            return ris;
        }
        public object GetVoltiRegistrati(string key, string start, string numero, string data)
        {
            object obj = "";
            httpPostRequest getVoltiRegistrati = new httpPostRequest();
            obj = getVoltiRegistrati.HttpGetVoltiRegistrati(key, start, numero, data);
            return obj;
        }

        public string DeleteVoltoRegistrato(string key, string id)
        {
            string ris = "";
            httpPostRequest delVoltoReg = new httpPostRequest();
            ris = delVoltoReg.HttpDeleteVoltoRegistrato(key, id);
            return ris;
        }

        public string AddDispositivo(string key, string nome, string tipo, string ip, string acceso)
        {
            string ris = "";
            httpPostRequest addDispositivo = new httpPostRequest();
            ris = addDispositivo.HttpAddDispositivo(key, nome, tipo, ip, acceso);
            return ris;
        }

        public object GetDispositivi(string key, string start, string numero, string data, string tipo)
        {
            object obj = "";
            httpPostRequest getDispositivi = new httpPostRequest();
            obj = getDispositivi.HttpGetDispositivi(key, start, numero, data, tipo);
            return obj;
        }

        public string DeleteDispositivo(string key, string id)
        {
            string ris = "";
            httpPostRequest delDispositvo = new httpPostRequest();
            ris = delDispositvo.HttpDeleteDispositivo(key, id);
            return ris;
        }

        public string AddSkill(string key, string nome, string descrizione, string azione, string idEmozione)
        {
            string ris = "";
            httpPostRequest addSkill = new httpPostRequest();
            ris = addSkill.HttpAddSkill(key, nome, descrizione, azione, idEmozione);
            return ris;
        }

        public object GetSkills(string key, string start, string numero, string data)
        {
            object obj = "";
            httpPostRequest getSkill = new httpPostRequest();
            obj = getSkill.HttpGetSkills(key, start, numero, data);
            return obj;
        }

        public string DeleteSkill(string key, string id)
        {
            string ris = "";
            httpPostRequest delSkill = new httpPostRequest();
            ris = delSkill.HttpDeleteSkill(key, id);
            return ris;
        }









    }
}
