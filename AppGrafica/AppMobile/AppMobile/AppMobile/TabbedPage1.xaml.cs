﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            InitializeComponent();
        }
        public void addDisp(object sender, EventArgs args)
        {
            App.Current.MainPage = new addDispositivo();
        }
        public void removeDisp(object sender, EventArgs args)
        {
            //// TODO ////
        }
        public void addPers(object sender, EventArgs args)
        {
            App.Current.MainPage = new addPersona();
        }
        public void removePers(object sender, EventArgs args)
        {
            //// TODO ////
        }
        public void addSkill(object sender, EventArgs args)
        {
            App.Current.MainPage = new addSkill();
        }
    }
}