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
        ViewModel vm = new ViewModel();
        public GraficiPage()
        {
            InitializeComponent();
        }

        public void EmChange(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            ViewModel vm = new ViewModel(picker.SelectedIndex.ToString());
            TabbedPage1 tabbedPage1 = new TabbedPage1();
            tabbedPage1.CurrentPage = tabbedPage1.Children[2];
            (App.Current as App).MainPage = tabbedPage1;
        }

        async void RefreshView_Refreshing(System.Object sender, System.EventArgs e)
        {
            await Task.Delay(3000);
            TabbedPage1 tabbedPage1 = new TabbedPage1();
            tabbedPage1.CurrentPage = tabbedPage1.Children[2];
            (App.Current as App).MainPage = tabbedPage1;
        }
    }
}