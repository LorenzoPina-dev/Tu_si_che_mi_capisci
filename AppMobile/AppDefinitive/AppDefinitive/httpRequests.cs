using System.Text;
using System.Net;
using System.Collections.Specialized;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace proj
{
    class httpRequests
    {
        WebClient wb = new WebClient();
        NameValueCollection data;
        JsonClass json = new JsonClass();
        string host = "80.22.36.186";
        public httpRequests() { }
        public string HttpRequestLogin(string username, string password) //login
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/login";

            /*set username e password*/
            data["username"] = username;
            data["password"] = password;

            var response = wb.UploadValues(url, "POST", data); //richiesta post
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string key = json.GetDataLogin(obj);
            return key;
        }

        public string HttpRequestSignin(string username, string password, string password2, string mail) //registrazione
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/register";
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

        public string HttpRequestForgotPwd(string mail) //richiesta codice reset pwd
        {
            string url = "http://" + host + "/resetPassword?mail=" + mail; 
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj); //codice
            return ris;
        }

        public string HttpRequestResetPwd(string cod, string pass, string pass2) //salvataggio nuova pwd
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/cambiaPassword"; 
            data["codice"] = cod;
            data["password"] = pass;
            data["password2"] = pass2;
            var response = wb.UploadValues(url, "PUT", data); //richiesta put
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj); 
            return ris;
        }

        public async Task<string> HttpRequestChangeInfoAsync(string key, string user, string pass, string mail, string img) //cambio info utente
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/utente/cambiaInfo";
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            FileStream fs = File.OpenRead(img);
            StreamContent streamContent = new StreamContent(fs);
            ByteArrayContent fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            // "file" parameter name should be the same as the server side input parameter name
            form.Add(fileContent, "immagine", Path.GetFileName(img));
            form.Add(new StringContent(user), "username");
            form.Add(new StringContent(pass), "password");
            form.Add(new StringContent(mail), "mail");
            HttpResponseMessage response = await httpClient.PutAsync(url, form);

            string result = System.Text.Encoding.UTF8.GetString(await response.Content.ReadAsByteArrayAsync());
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        public object HttpRequestInfo(string key) //ottieni info utente
        {
            string url = "http://" + host + "/" + key + "/utente"; 
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = JObject.Parse(result);
            object objRis = json.GetInfo(obj); //chiedo info
            string ris = "";
            if (objRis.GetType() == typeof(string)) //l'utente non c'è quindi mi ritorna una stringa
            {
                ris = json.GetText(obj);
                return ris; //ritorno la stringa
            }
            else //l'utente è presente
            {
                string[] array = (string[])objRis; //vettore con info
                object obj3 = GetImage(key, "utente", array[2]);
                if(obj3.GetType() == typeof(string)) //errore
                {
                    JObject obj4 = JObject.Parse(result);
                    array[4] = json.GetText(obj4);
                }
                return array;
            }
        }

        public string HttpRequestAddEmozione(string key, string tipo, string dataRil, string oraRil, string idDisp) //aggiungi emozione
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/emozioni/add"; 
            data["tipo"] = tipo;
            data["dataRilevazione"] = dataRil;
            data["ora"] = oraRil;
            data["idDispositivo"] = idDisp;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpRequestGetEmozioni(string key, string start, string numero, string tipo, string data) //prendi emozioni
        {
            string url = "http://" + host + "/" + key + "/emozioni/?start=" + start + "&numero=" + numero + "&tipo=" + tipo + "&data=" + data;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = JObject.Parse(result);
            string ris = "";
            json.GetEmozioni(obj);
            return ris;
        }

        public string HttpRequestAddVoltoTrovato(string key, string img, string dataRil, string oraRil, string idDisp, string idVolto) //add volto trovato
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/voltoTrovato/add";
            data["immagine"] = img;
            data["dataRilevazione"] = dataRil;
            data["ora"] = oraRil;
            data["idDispositivo"] = idDisp;
            data["idVolto"] = idVolto;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpRequestGetVoltiTrovati(string key, string start, string numero, string data) //prendo i volti trovati
        {
            string url = "http://" + host + "/" + key + "/voltoTrovato/?start=" + start + "&numero=" + numero + "&data=" + data;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.Unicode.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }
        public string HttpRequestAddVoltoRegistrato(string key, string img, string nome) //aggiungi volto registrato
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/voltoRegistrato/add";
            data["immagine"] = img;
            data["nome"] = nome;
            var response = wb.UploadValues(url, "POST", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpRequestGetVoltiRegistrati(string key, string start, string numero, string data) //prendi volti registrati
        {
            string url = "http://" + host + "/" + key + "/voltoRegistrato/?start=" + start + "&numero=" + numero + "&data=" + data;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }

        public string HttpRequestDeleteVoltoRegistrato(string key, string id) //cancella volto registrato
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/voltoRegistrato/remove";
            data["id"] = id;
            var response = wb.UploadValues(url, "DELETE", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        public string HttpRequestAddDispositivo(string key, string nome, string tipo, string ip, string acceso) //aggiungi dispositivo
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/dispositivi/add";
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
        public object HttpRequestGetDispositivi(string key, string max) //prendi dispositivi
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/dispositivi/?max=" + max;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JArray obj = json.ParseArray(result);
            object ris = json.GetDispo(obj);
            return ris;
        }

        public string HttpRequestDeleteDispositivo(string key, string id) //elimina dispositivo
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/dispositivi/remove";
            data["id"] = id;
            var response = wb.UploadValues(url, "DELETE", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        public string HttpRequestAddSkill(string key, string nome, string descrizione, string azione, string idEmozione) //aggiungi skill
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/skill/add/?nome=" + nome + "&descrizione=" + descrizione + "&azione=" + azione + "&idEmozione=" + idEmozione;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = JObject.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        //da rivedere
        public object HttpRequestGetSkills(string key, string start, string numero, string dataInizio) //prendi dispositivi
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/skill/?start=" + start + "&numero=" + numero + "&data=" + dataInizio;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response);
            JObject obj = JObject.Parse(result);
            object ris = json.GetInfo(obj);
            return ris;
        }

        public string HttpRequestDeleteSkill(string key, string id) //elimina dispositivo
        {
            data = new NameValueCollection();
            string url = "http://" + host + "/" + key + "/skill/remove";
            data["id"] = id;
            var response = wb.UploadValues(url, "DELETE", data);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            JObject obj = json.Parse(result);
            string ris = json.GetText(obj);
            return ris;
        }

        private object GetImage(string key, string tabella, string nome)
        {
            WebClient wb = new WebClient();
            string url = "http://" + host + "/" + key + "/immagine/" + tabella + "?nomefile=" + nome;
            var response = wb.DownloadData(url);
            string result = System.Text.Encoding.UTF8.GetString(response); //ottengo una risposta
            try
            {
                JObject obj = json.Parse(result);
                string ris = json.GetText(obj);
                return ris;
            }
            catch (Exception ){
                File.WriteAllBytes("./img.png", response);
                return 0;
            }
        }
    }
}
