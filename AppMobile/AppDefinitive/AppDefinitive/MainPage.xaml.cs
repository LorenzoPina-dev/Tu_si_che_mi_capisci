using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppDefinitive
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void LoginEntra(object sender, EventArgs args) {
            // questa Action manderà quindi alla schermata principale, con la barra sotto 
            utente ut = new utente(); 
            //TODO: CONTROLLI//
            string s = "";
            s = mail.Text;

            string p = pass.Text;
            // così dovrebbe prendere i dati dalle entry, e se non sono validi non fa entrare nell'app

            if (s != null)
            {// facciamo una prova
                App.Current.MainPage = new TabbedPage1(ut); // <- manda alla tabbed page
            }else 
            {
                error.Text = "Errore, login Errata"; 
            }
        }

        public void registerPage(object sender, EventArgs args){
            App.Current.MainPage = new Register(); 
        }
    }
}

