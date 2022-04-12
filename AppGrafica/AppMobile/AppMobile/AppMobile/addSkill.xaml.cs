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
    public partial class addSkill : ContentPage
    {
        public addSkill()
        {
            InitializeComponent();

            lista.ItemsSource = "Gay"; 
        }
        public void indietro(object sender, EventArgs args)
        {
            App.Current.MainPage = new TabbedPage1();
        }
    }
}