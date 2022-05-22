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
    public partial class ForgotPwdPage
    {
        Utente utente;
        public ForgotPwdPage()
        {
            InitializeComponent();
            utente = new Utente();
        }

        public void BackLogin(object sender, EventArgs args)
        {
            App.Current.MainPage = new LoginPage();
        }

        public void BtnInvia(object sender, EventArgs args)
        {
            string mail = Email.Text;
            if (mail != null)
            {
                string ris = utente.ForgotPwd(mail);
                if (!ris.Contains("errata"))
                    App.Current.MainPage = new ResetPwdPage();
                else
                    error.Text = ris;
            }
            else
                error.Text = "compilare il campo!";
        }

    }
}
