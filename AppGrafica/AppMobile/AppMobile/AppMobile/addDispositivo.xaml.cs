﻿using proj;
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
    public partial class addDispositivo : ContentPage
    {
        Utente utente;
        public addDispositivo()
        {
            utente = new Utente();
            InitializeComponent();
        }

        public void Indietro(object sender, EventArgs args)
        {
            App.Current.MainPage = new TabbedPage1();
        }

        public void Aggiungi(object sender, EventArgs args)
        {
            string tipo = "";
            string nome = Nome.Text.ToString();
            string ip = Ip.Text.ToString();
            if (Mic.IsChecked == true)
                tipo = "1";
            else
                tipo = "0";
            if (nome != "" && ip != "")
            {
                utente.AddDispositivo(nome, tipo, ip);
                App.Current.MainPage = new TabbedPage1();
            }
            else
                error.Text = "compilare tutti i campi";
            
        }

        public void ChangeMic(object sender, EventArgs args)
        {
            Cam.IsChecked = false;
            Mic.IsChecked = true;
        }
    }
}