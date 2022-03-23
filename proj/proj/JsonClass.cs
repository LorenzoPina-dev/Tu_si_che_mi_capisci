using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace proj
{
    class JsonClass
    {
        public JsonClass(){}

        public JObject Parse(string result)
        {
            JObject objects = JObject.Parse(result);
            return objects;
        }

        public string GetDataLogin(JObject obj)
        {
            string ris = "";
            bool success = obj.SelectToken("success").Value<bool>();
            if (success == true)
                ris = obj["result"]["ApiKey"].ToString();
            else
                ris = obj["result"]["testo"].ToString();
            return ris;
        }

        public string GetText(JObject obj)
        {
            string ris = "";
            ris = obj["result"]["testo"].ToString();
            return ris;
        }

        public object GetInfo(JObject obj)
        {
            object ris = "";
            string user = "", xp = "", mail = "", img = "";
            bool success = obj.SelectToken("success").Value<bool>();
            if (success == true)
            {
                ris = (string[])(object)obj;
                user = Convert.ToString(obj["result"]["utente"]["Username"]);
                mail = Convert.ToString(obj["result"]["utente"]["Email"]);
                img = Convert.ToString(obj["result"]["utente"]["Immagine"]);
                xp = Convert.ToString(obj["result"]["utente"]["Xp"]);
                ris[0] = user;
                ris[1] = mail;
                ris[2] = img;
                ris[3] = xp;
            }
            else
                ris = obj["result"]["testo"].ToString();
            return ris;
        }

    }
}
