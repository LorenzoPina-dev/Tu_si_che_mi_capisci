using System.Text;
using System.Net;
using System.Text.Json;
using System.Collections.Specialized;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace proj
{
    class httpPostRequest
    {
        WebClient wb = new WebClient();
        NameValueCollection data;
        JsonClass json = new JsonClass();
        public httpPostRequest() { }
        public string HttpPostRequestLogin(string username, string password) //login
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51:3000/login/";

            /*set username e password*/
            data["username"] = username;
            data["password"] = password;

            var response = wb.UploadValues(url, "POST", data); //richiesta post
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string key = json.GetDataLogin(obj);
            return key;
        }

        public string HttpPostRequestSignin(string username, string password, string password2, string mail) //registrazione
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51:3000/register/";
            data["username"] = username;
            data["password"] = password;
            data["password2"] = password2;
            data["mail"] = mail;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        public string HttpGetRequestForgotPwd(string mail) //richiesta codice reset pwd
        {
            string url = "http://172.16.102.51:3000/resetPassword?mail=" + mail; 
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj); //codice
            return ris;
        }

        public string HttpGetRequestResetPwd(string cod, string pass, string pass2) //salvataggio nuova pwd
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51:3000/cambiaPassword/"; 
            data["codice"] = cod;
            data["pass"] = pass;
            data["pass2"] = pass2;
            var response = wb.UploadValues(url, "PUT", data); //richiesta put
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj); 
            return ris;
        }

        public string HttpGetRequestChangeInfo(string key, string user, string pass, string mail, string img) //cambio info utente
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51:3000/" + key + "/utente/cambiaInfo/";
            data["user"] = user;
            data["pass"] = pass;
            data["mail"] = mail;
            data["img"] = img;
            var response = wb.UploadValues(url, "POST", data); //richiesta put
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        public object HttpGetRequestInfo(string key) //ottieni info utente
        {
            string url = "http://172.16.102.51:3000/" + key + "/utente/"; 
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }

        public string HttpAddEmozione(string key, string tipo, string dataRil, string idDisp) //aggiungi emozione
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/emozioni/add/"; 
            data["tipo"] = tipo;
            data["dataRil"] = dataRil;
            data["idDisp"] = idDisp;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpGetEmozioni(string key, string start, string numero, string tipo, string data) //prendi emozioni
        {
            string url = "http://172.16.102.51/" + key + "/emozioni/?start=" + start + "&numero=" + numero + "&tipo=" + tipo + "&data=" + data;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }

        public string HttpAddVoltoTrovato(string key, string img, string dataRil, string idDisp, string idVolto) //add volto trovato
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/voltoTrovato/add/";
            data["img"] = img;
            data["dataRil"] = dataRil;
            data["idDisp"] = idDisp;
            data["idVolto"] = idVolto;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpGetVoltiTrovati(string key, string start, string numero, string data) //prendo i volti trovati
        {
            string url = "http://172.16.102.51/" + key + "/voltoTrovato/?start=" + start + "&numero=" + numero + "&data=" + data;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }
        public string HttpAddVoltoRegistrato(string key, string img, string nome) //aggiungi volto registrato
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/voltoRegistrato/add/";
            data["img"] = img;
            data["nome"] = nome;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpGetVoltiRegistrati(string key, string start, string numero, string data) //prendi volti registrati
        {
            string url = "http://172.16.102.51/" + key + "/voltoRegistrato/?start=" + start + "&numero=" + numero + "&data=" + data;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }

        public string HttpDeleteVoltoRegistrato(string key, string id) //cancella volto registrato
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/voltoRegistrato/remove/?id" + id;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        public string HttpAddDispositivo(string key, string nome, string tipo, string ip, string acceso) //aggiungi dispositivo
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/dispositivi/add/";
            data["nome"] = nome;
            data["tipo"] = tipo;
            data["ip"] = ip;
            data["acceso"] = acceso;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpGetDispositivi(string key, string start, string numero, string dataInizio, string tipo) //prendi dispositivi
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/dispositivi/?start=" + start + "&numero=" + numero + "&data=" + dataInizio + "&tipo=" + tipo;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }

        public string HttpDeleteDispositivo(string key, string id) //elimina dispositivo
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/dispositivi/remove/?id=" + id;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        public string HttpAddSkill(string key, string nome, string descrizione, string azione, string idEmozione) //aggiungi skill
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/skill/add/?nome=" + nome + "&descrizione=" + descrizione + "&azione=" + azione + "&idEmozione=" + idEmozione;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpGetSkills(string key, string start, string numero, string dataInizio) //prendi dispositivi
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/skill/?start=" + start + "&numero=" + numero + "&data=" + dataInizio;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }

        public string HttpDeleteSkill(string key, string id) //elimina dispositivo
        {
            data = new NameValueCollection();
            string url = "http://172.16.102.51/" + key + "/skill/remove/?id=" + id;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }
    }
}
