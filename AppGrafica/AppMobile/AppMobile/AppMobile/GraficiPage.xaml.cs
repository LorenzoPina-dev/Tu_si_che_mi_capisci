using Newtonsoft.Json.Linq;
using proj;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraficiPage : ContentPage
    {
        Syncfusion.SfChart.XForms.SfChart[] vett;
        public GraficiPage()
        {
            InitializeComponent();
            vett = new Syncfusion.SfChart.XForms.SfChart[7] { lineChartArr, lineChartFel, lineChartTriste, lineChartDisg, lineChartSpav, lineChartSorpr, lineChartNeutro };
        }

        public void EmChange(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            int index = picker.SelectedIndex;
            
            for(int i = 0; i < vett.Length; i++)
            {
                if (index == i)
                    vett[i].IsVisible = true;
                else
                    vett[i].IsVisible = false;
            }
        }

        async void RefreshView_Refreshing(System.Object sender, System.EventArgs e)
        {
            await Task.Delay(3000);
            ViewModel vm = new ViewModel();
            TabbedPage1 tabbedPage1 = new TabbedPage1();
            tabbedPage1.CurrentPage = tabbedPage1.Children[2];
            (App.Current as App).MainPage = tabbedPage1;
        }
    }
}