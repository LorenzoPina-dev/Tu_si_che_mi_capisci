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
        //ViewModel vm = new ViewModel();
        public GraficiPage()
        {
            InitializeComponent();
            
        }

        public void EmChange(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            //vm.FillDataLine(picker.SelectedIndex.ToString());
        }
    }
}