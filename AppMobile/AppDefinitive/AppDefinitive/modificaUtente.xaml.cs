using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDefinitive
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class modificaUtente : ContentPage
    {
        utente ut = new utente(); 
        
        public modificaUtente()
        {
            InitializeComponent();
        }

        public modificaUtente(utente ut)
        {
            InitializeComponent();      

            this.ut = ut;

            try
            {
                string[] ris = (string[])ut.GetInfo(ut.key);
                string username = ris[0];
                string email = ris[1];
                string imm = ris[2];


                prova.Text = email; 
                mail.Text = email;
                user.Text = username;
                immagine.Source = imm; 
            }
            catch (Exception ex) { 
                

            }
        }

        public void cambiaInfo(object sender, EventArgs args)
        {

            string usern = user.Text;
            string passw = pass.Text;
            string email = mail.Text; 
            string imgSource = immagine.Source.ToString();
            string key = ut.key;

            ut.ChangeInfo(key, usern, passw, email, imgSource);

        }
    }
}