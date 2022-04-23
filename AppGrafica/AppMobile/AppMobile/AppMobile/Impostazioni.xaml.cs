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
    public partial class Impostazioni : ContentPage
    {
        public Impostazioni()
        {
            InitializeComponent();
        }

        public void indietro(object sender, EventArgs args)
        {
            // apre una nuova finestra senza fare cose strane, poi lo tolgo
            // SOLO PER TESTARE

            App.Current.MainPage = new LoginPage();
        }
    }
}