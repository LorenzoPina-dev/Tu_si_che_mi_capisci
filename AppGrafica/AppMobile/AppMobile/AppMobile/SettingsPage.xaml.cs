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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public IList<string> Permessi
        {
            get
            {
                return new List<string> { "Arrabbiato", "Felice", "Triste", "Disgustato", "Spaventato", "Sorpreso", "Neutro" };
            }
        }

        public void ChangePermesso(object sender, EventArgs e) { }
    }
}