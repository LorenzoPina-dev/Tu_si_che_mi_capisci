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
            /*string username = Username.Text, password = Password.Text;
            if (username != null && password != null) //se sono vuoti non controllo
            {
                string ris = utente.Login(username, password);
                if (ris == "ok")
                {
                    App.Current.MainPage = new InfoUtentePage();
                }
                else
                    error.Text = ris;
            }
            else
                error.Text = "compilare tutti i campi!";*/
            App.Current.MainPage = new TabbedPage1();
            error.Text = "compilare tutti i campi!"; 
        }
    }
}
