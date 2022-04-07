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
            string smail = mail.Text; 

            string p = pass.Text;


            string ris = ut.Login(smail, p);
            if (ris.Contains("errore"))
                error.Text = ris;
            else 
                App.Current.MainPage = new TabbedPage1(ut); // <- manda alla tabbed page
             
            
                
            
        }

        public void registerPage(object sender, EventArgs args){
            App.Current.MainPage = new Register(); 
        }
    }
}

