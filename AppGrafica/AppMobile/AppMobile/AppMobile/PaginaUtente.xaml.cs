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
    public partial class PaginaUntente : ContentPage
    {
        public PaginaUntente()
        {
            InitializeComponent();
        }

        public void impostazioniPagina(object sender, EventArgs args)
        {

            App.Current.MainPage = new InfoUtentePage(); 
        }
    }
}