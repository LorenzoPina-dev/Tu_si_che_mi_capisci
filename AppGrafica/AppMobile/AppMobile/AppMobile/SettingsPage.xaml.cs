using Newtonsoft.Json.Linq;
using proj;
using System;
using System.Collections.Generic;
using System.IO;
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
        List<JObject> list, list2;
        public SettingsPage()
        {
            utente = new Utente();
            InitializeComponent();
            Request();
        }

        public void ChangePermesso(object sender, EventArgs e) 
        {
            string foto = "";
            string path = "";
            var picker = sender as Picker;
            nomePerm.Text = picker.SelectedItem.ToString();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i]["Nome"].ToString() == picker.SelectedItem.ToString())
                {
                    foto = list[i]["Immagine"].ToString();
                    imgPerm.Source = ImageSource.FromStream(() => new MemoryStream(utente.GetImage("voltoregistrato", foto)));
                }
            }
        }

        public void ChangeDisp(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            nomeDisp.Text = disp.SelectedItem.ToString();
            for (int i = 0; i < list2.Count; i++)
            {
                if (list2[i]["Nome"].ToString() == picker.SelectedItem.ToString())
                {
                    if (list2[i]["Tipo"].ToString() == "1")
                        imgDisp.Source = "mic.png";
                    else
                        imgDisp.Source = "cam.jpg";

                    if (list2[i]["Acceso"].ToString() == "1")
                        attivo.IsChecked = true;
                    else
                        attivo.IsChecked = false;
                }
            }
        }

        public void Request()
        {
            list = (List<JObject>)utente.GetVoltiRegistrati();
            for(int i = 0; i < list.Count; i++)
            {
                permessi.Items.Add(list[i]["Nome"].ToString());
            }

            list2 = (List<JObject>)utente.GetDispositivi();
            for (int i = 0; i < list2.Count; i++)
            {
                disp.Items.Add(list2[i]["Nome"].ToString());
            }
        }

        public void AddDisp(object sender, EventArgs e)
        {
            Device.SetFlags(new[] { "RadioButton_Experimental" });
            App.Current.MainPage = new addDispositivo();
        }

        public void AddPerm(object sender, EventArgs e)
        {
            App.Current.MainPage = new addPersona();
        }

        public void RemoveDisp(object sender, EventArgs e)
        {
            string id = "";
            if (disp.SelectedIndex != -1)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i]["Nome"].ToString() == disp.SelectedItem.ToString())
                    {
                        id = list2[i]["Id"].ToString();
                        utente.DeleteDispositivo(id);
                        App.Current.MainPage = new SettingsPage();
                    }
                }
            }
            else
                errorDisp.Text = "Selezionare un dispositivo per rimuoverlo";
        }

        public void RemovePerm(object sender, EventArgs e)
        {
            string id = "";
            if(permessi.SelectedIndex != -1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i]["Nome"].ToString() == permessi.SelectedItem.ToString())
                    {
                        id = list[i]["Id"].ToString();
                        utente.DeleteVoltoRegistrato(id);
                        App.Current.MainPage = new SettingsPage();
                    }
                }
            }
            else
                errorPerm.Text = "Selezionare un permesso per rimuoverlo";
        }
    }
}