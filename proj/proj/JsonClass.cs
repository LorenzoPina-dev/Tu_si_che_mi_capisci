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
                ris = obj["error"]["testo"].ToString();
            return ris;
        }

        public string GetText(JObject obj)
        {
            string ris = "";
            bool success = obj.SelectToken("success").Value<bool>();
            if (success == true)
                ris = obj["result"]["testo"].ToString();
            else
                ris = obj["error"]["testo"].ToString();
            return ris;
        }

        public object GetInfo(JObject obj)
        {
            string ris = "", user = "", password = "", mail = "", img = "";
            string[] array = new string[4];
            bool success = obj.SelectToken("success").Value<bool>();
            if (success == true)
            {
                user = Convert.ToString(obj["result"]["username"]);
                password = Convert.ToString(obj["result"]["password"]);
                mail = Convert.ToString(obj["result"]["mail"]);
                img = Convert.ToString(obj["result"]["img"]);
                array[0] = user;
                array[1] = password;
                array[2] = mail;
                array[3] = img;
            }
            else
                ris = obj["error"]["testo"].ToString();
            return ris;
        }

    }
}
