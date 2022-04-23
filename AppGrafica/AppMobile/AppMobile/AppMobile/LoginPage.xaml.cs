using proj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppMobile
{
    public partial class LoginPage : ContentPage
    {
        Utente utente;
        public LoginPage()
        {
            InitializeComponent();
            utente = new Utente();
        }
        
        public void ForgotPwd(object sender, EventArgs args)
        {
            App.Current.MainPage = new ForgotPwdPage();
        }

        public void SignIn(object sender, EventArgs args)
        {
            App.Current.MainPage = new SignInPage();
        }
        public void BtnAccedi(object sender, EventArgs args)
        {
            
            string username = Username.Text, password = Password.Text;
            if (username != null && password != null) //se sono vuoti non controllo
            {
                string ris = utente.Login(username, password);
                if (ris.Contains("-"))
                {
                    App.Current.MainPage = new TabbedPage1();
                }
                else
                    error.Text = ris;
            }
            else
                error.Text = "compilare tutti i campi!";            
            // bisogna per forza compilare i campi, altrimenti crasha chiamando solo la finestrta

        }

        public void testingProd(object sender, EventArgs args) {
            // apre una nuova finestra senza fare cose strane, poi lo tolgo
            // SOLO PER TESTARE

            App.Current.MainPage = new PaginaUntente(); 
        }
    }
}
