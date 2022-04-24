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
    public partial class addDispositivo : ContentPage
    {
        Utente utente;
        public addDispositivo()
        {
            utente = new Utente();
            InitializeComponent();
        }

        public void Indietro(object sender, EventArgs args)
        {
            App.Current.MainPage = new SettingsPage();
        }

        public void Aggiungi(object sender, EventArgs args)
        {
            string tipo = "";
            string nome = Nome.Text;
            string ip = Ip.Text;
            if (Mic.IsChecked == true)
                tipo = "1";
            else
                tipo = "0";
            utente.AddDispositivo(nome, tipo, ip);
            App.Current.MainPage = new SettingsPage();
        }

        public void ChangeMic(object sender, EventArgs args)
        {
            Cam.IsChecked = false;
            Mic.IsChecked = true;
        }
    }
}