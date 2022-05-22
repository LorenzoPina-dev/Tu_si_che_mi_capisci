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

        public JArray ParseArray(string obj)
        {
            JArray objects = JArray.Parse(obj);
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
            ris += obj["result"]["testo"].ToString();
            return ris;
        }

        public object GetInfo(JObject obj)
        {
            bool success = obj.SelectToken("success").Value<bool>();
            object ris;
            if (success == true)
            {
                string[] array = new string[4];

                string user = Convert.ToString(obj["result"]["utente"]["Username"]);
                string mail = Convert.ToString(obj["result"]["utente"]["Email"]);
                string img = Convert.ToString(obj["result"]["utente"]["Immagine"]);
                string xp = Convert.ToString(obj["result"]["utente"]["Xp"]);
                array[0] = user;
                array[1] = mail;
                array[2] = img;
                array[3] = xp;
                return array;
            }
            else
                ris = obj["result"]["testo"].ToString();
            return ris;
        }

        public object GetObjArray(JObject obj, string parametro)
        {
            object ris;
            bool success = obj.SelectToken("success").Value<bool>();
            if(success == true)
            {
                JArray jArray = (JArray)obj["result"][parametro];
                List<JObject> list = new List<JObject>();
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject obj2 = (JObject)jArray[i];
                    list.Add(obj2);
                }
                return list;
            }
            else
                ris = obj["result"]["testo"].ToString();
            return ris;
        }
    }
}
