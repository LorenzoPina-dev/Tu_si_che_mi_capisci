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
    public partial class SignInPage : ContentPage
    {
        Utente utente;
        public SignInPage()
        {
            InitializeComponent();
            utente = new Utente();
        }

        public void BtnSignIn(object sender, EventArgs args)
        {
            string mail = Email.Text, username = Username.Text, password = Password.Text, password2 = Password2.Text;
            if (mail != null && username != null && password != null && password2 != null)
            {
                string ris = utente.Signin(mail, username, password, password2);
                if (ris == "ok")
                    App.Current.MainPage = new LoginPage();
                else
                    error.Text = ris;
            }
            else
                error.Text = "compilare tutti i campi!";
        }

        public void BtnLogin(object sender, EventArgs args)
        {
            App.Current.MainPage = new LoginPage();
        }

    }
}
