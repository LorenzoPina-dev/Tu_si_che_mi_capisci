using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDefinitive
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        utente ut = new utente(); 
        public Register()
        {
            InitializeComponent();
        }

        public void registratiButton(object sender, EventArgs args)
        {
            string usern = user.Text; 
            string mail = email.Text;
            string passw = pass.Text; 
            string ris = ut.Signin(usern, passw, mail);
            if(ris != "")
                error.Text = ris;
            else
                App.Current.MainPage = new MainPage();
        }
        public void tornaLogin(object sender, EventArgs args) {
            App.Current.MainPage = new MainPage();
        }
    }
}