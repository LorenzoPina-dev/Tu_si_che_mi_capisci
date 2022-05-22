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
    public partial class ResetPwdPage : ContentPage
    {
        Utente utente;
        public ResetPwdPage()
        {
            InitializeComponent();
            utente = new Utente();
        }

        public void BackLogin(object sender, EventArgs args)
        {
            App.Current.MainPage = new LoginPage();
        }
        public void BtnCambia(object sender, EventArgs args)
        {
            string codice = Codice.Text, password = Password.Text, password2 = Password2.Text;
            if (codice != null && password != null && password2 != null) //se sono vuoti non controllo
            {
                string ris = utente.ResetPwd(codice, password, password2);
                if (!ris.Contains("corrispondono"))
                    App.Current.MainPage = new LoginPage();
                else
                    error.Text = ris;
            }
            else
                error.Text = "compilare tutti i campi!";
        }
    }
}
