using Newtonsoft.Json.Linq;
using proj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        Utente utente;
        List<JObject> list;
        public SettingsPage()
        {
            utente = new Utente();
            InitializeComponent();
            Request();
        }

        public void ChangePermesso(object sender, EventArgs e) 
        {
            string foto = "";
            var picker = sender as Picker;
            nome.Text = picker.SelectedItem.ToString();
            /*for(int i = 0; i < list.Count; i++)
            {
                if(list[i]["Nome"].ToString() == picker.SelectedItem.ToString())
                {
                    foto = list[i]["Nome"].ToString();
                    utente.GetImage("voltoRegistrato, foto);
                }
            }*/

            //esempio statico
            img.Source = picker.SelectedItem.ToString() + ".jpg";
        }

        public void ChangeDisp(object sender, EventArgs e)
        {

        }

        public void Request()
        {
            /*list = (List<JObject>)utente.GetVoltiRegistrati();
            for(int i = 0; i < list.Count; i++)
            {
                permessi.Items.Add(list[i]["Nome"].ToString());
            }*/

            //esempi statici
            permessi.Items.Add("lorenzo");
            permessi.Items.Add("pepe");
        }

        public void RemovePerm(object sender, EventArgs e)
        {
            string id = "";
            var picker = sender as Picker;
            /*for(int i = 0; i < list.Count; i++)
            {
                if(list[i]["Nome"].ToString() == picker.SelectedItem.ToString())
                {
                    id = list[i]["Id"].ToString();
                    utente.DeleteVoltoRegistrato(id);
                }
            }*/
        }

        public void AddDisp(object sender, EventArgs e)
        {
            App.Current.MainPage = new addDispositivo();
        }

        public void AddPerm(object sender, EventArgs e)
        {
            App.Current.MainPage = new addPersona();
        }

        public void RemoveDisp(object sender, EventArgs e)
        {
            string id = "";
            var picker = sender as Picker;
            /*for(int i = 0; i < list.Count; i++)
            {
                if(list[i]["Id"].ToString() == picker.SelectedItem.ToString())
                {
                    id = list[i]["Id"].ToString();
                    utente.DeleteDispositivo(id);
                }
            }*/
        }
    }
}