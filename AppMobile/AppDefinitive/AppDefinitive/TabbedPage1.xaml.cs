using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

namespace AppDefinitive
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        List<Entry> entries = new List<Entry>
        {
            new Entry(200){
                Label= "Prova 1",
                ValueLabel = "200"
            },
            new Entry(400){
                Label= "Prova 2",
                ValueLabel = "200"
            },
            new Entry(500){
                Label= "Prova 3,",
                ValueLabel = "200"
            },
        };

        utente ut = new utente();
        public TabbedPage1()
        {
            InitializeComponent();

            chartView.Chart = new BarChart() { Entries = entries };
        }
        public TabbedPage1(utente ut) {

            InitializeComponent();
            this.ut = ut;
            chartView.Chart = new BarChart() { Entries = entries };

            visualizzaSkill.Text = restituisciSkills(); 
        }

        public void tornaLogin(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
        }

        public string restituisciSkills() {

            string descrizione = ut.sLista[0].descrizione; 
            
            return descrizione; 
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}